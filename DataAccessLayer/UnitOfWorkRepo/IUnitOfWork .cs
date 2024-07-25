using DataAccessLayer.Repositories.AddessRepo;
using DataAccessLayer.Repositories.ProductRepo;
using DataAccessLayer.Repositories.ShoppingCartRepo;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace DataAccessLayer.UnitOfWorkRepo
{
    public interface IUnitOfWork
    {
        // Task BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.Serializable);
        Task<IDbContextTransaction> BeginTransactionAsync(System.Data.IsolationLevel isolationLevel);
        void Commit();
        void Rollback();
        int Complete();
        IProductRepository productRepository { get; }
        IAddressRepo addressRepo { get; }
        IShoppiingCartRepo shoppiingCartRepo { get; }
    }
}
