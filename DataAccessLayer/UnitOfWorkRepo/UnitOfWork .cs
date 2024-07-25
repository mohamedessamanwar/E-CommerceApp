using DataAccessLayer.Data.Context;
using DataAccessLayer.Repositories.AddessRepo;
using DataAccessLayer.Repositories.ProductRepo;
using DataAccessLayer.Repositories.ShoppingCartRepo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace DataAccessLayer.UnitOfWorkRepo
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ECommerceContext _context;
        public IProductRepository productRepository { get; }
        public IAddressRepo addressRepo { get; }
        public IShoppiingCartRepo shoppiingCartRepo { get; }
       // private IDbTransaction _transaction;
        public UnitOfWork(ECommerceContext context)
        {
            this._context = context;
            this.productRepository = new ProductRepository(context);
            this.addressRepo = new AddressRepo(_context);
            this.shoppiingCartRepo = new ShoppingCartRepo(_context);
        }
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _transaction?.Dispose();
            GC.SuppressFinalize(this);
        }

        private IDbContextTransaction _transaction; // Add this field to your class

        //public async Task BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.Serializable)
        //{
        //    if (_context.Database.CurrentTransaction != null)
        //    {
        //        return;
        //    }

        //    await _context.Database.ExecuteSqlRawAsync($"SET TRANSACTION ISOLATION LEVEL {isolationLevel}");
        //    _transaction = await _context.Database.BeginTransactionAsync();
        //}
        public async Task<IDbContextTransaction> BeginTransactionAsync(System.Data.IsolationLevel isolationLevel)
        {
            _transaction = await _context.Database.BeginTransactionAsync(isolationLevel);
            return _transaction;
        }

        public void Commit()
        {
            try
            {
                _transaction?.Commit();
            }
            catch
            {
                _transaction?.Rollback();
                throw;
            }
            finally
            {
                _transaction?.Dispose();
                _transaction = null; // Reset the transaction field after disposal
            }
        }

        public void Rollback()
        {
            try
            {
                _transaction?.Rollback();
            }
            finally
            {
                _transaction?.Dispose();
            }
        }




    }
}
