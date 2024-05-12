using DataAccessLayer.Data.Context;
using DataAccessLayer.Repositories.ProductRepo;

namespace DataAccessLayer.UnitOfWorkRepo
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ECommerceContext _context;
        public IProductRepository productRepository { get; }
        public UnitOfWork(ECommerceContext context, IProductRepository productRepository)
        {
            this._context = context;
            this.productRepository = productRepository;
        }



        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {

            GC.SuppressFinalize(this);
        }
    }
}
