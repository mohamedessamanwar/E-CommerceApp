using DataAccessLayer.Repositories.ProductRepo;

namespace DataAccessLayer.UnitOfWorkRepo
{
    public interface IUnitOfWork
    {
        int Complete();
        IProductRepository productRepository { get; }
    }
}
