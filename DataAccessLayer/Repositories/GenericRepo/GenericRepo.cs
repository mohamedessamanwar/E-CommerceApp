using DataAccessLayer.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.GenericRepo
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        protected readonly ECommerceContext context;
        public GenericRepo(ECommerceContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            return await context.Set<T>().AsNoTracking().ToListAsync();
        }
        public async Task<T?> GetByIdAsync(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }
        public async Task AddAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
        }
        public void Update(T entity)
        {

             context.Set<T>().Update(entity);
        }
        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);

        }
        public void DeleteBulk(IEnumerable<T> entities)
        {
            context.Set<T>().RemoveRange(entities);
        }



    }
}
