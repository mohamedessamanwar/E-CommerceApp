using DataAccessLayer.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.GenericRepo
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        private readonly ECommerceContext context;
        public GenericRepo(ECommerceContext context)
        {
            this.context = context;
        }
        public IEnumerable<T> GetAll()
        {
            return context.Set<T>().AsNoTracking();
        }
        public T? GetById(int id)
        {
            return context.Set<T>().Find(id);
        }
        public async Task AddAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
        }
        public void Update(T entity)
        {

        }
        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);

        }
    }
}
