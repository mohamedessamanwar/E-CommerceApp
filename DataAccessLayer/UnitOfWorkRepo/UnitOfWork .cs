using DataAccessLayer.Data.Context;

namespace DataAccessLayer.UnitOfWorkRepo
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ECommerceContext _context;

        public UnitOfWork(ECommerceContext context)
        {
            this._context = context;

        }



        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
