using DataAccessLayer.Data.Context;
using DataAccessLayer.Repositories.AddessRepo;
using DataAccessLayer.Repositories.OrderItemRepo;
using DataAccessLayer.Repositories.OrderRepo;
using DataAccessLayer.Repositories.ProductRepo;
using DataAccessLayer.Repositories.ReviewRepo;
using DataAccessLayer.Repositories.ShoppingCartRepo;
using DataAccessLayer.Repositories.UserTokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace DataAccessLayer.UnitOfWorkRepo
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ECommerceContext _context;
        private readonly DapperContext dapperContext; 
        public IProductRepository productRepository { get; }
        public IAddressRepo addressRepo { get; }
        public IShoppiingCartRepo shoppiingCartRepo { get; }
        public IOrderRepo orderRepo { get; }
        public IOrderItemRepo orderItemRepo { get; }

        public IReviewRepo reviewRepo {  get; }
         public IUserTokenRepo userToken { get; }




        public UnitOfWork(ECommerceContext context, DapperContext dapperContext)
        {
            this._context = context;
            this.dapperContext = dapperContext;
            this.productRepository = new ProductRepository(context, dapperContext);
            this.addressRepo = new AddressRepo(_context);
            this.shoppiingCartRepo = new ShoppingCartRepo(_context);
            this.orderRepo = new OrderRepo(_context);
            orderItemRepo = new OrderItemRepo(_context);
            reviewRepo = new ReviewRepo(_context);
            userToken = new UserTokenRepo(_context);
          
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
