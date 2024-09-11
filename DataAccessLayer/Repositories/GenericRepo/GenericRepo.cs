using DataAccessLayer.Data.Context;
using DataAccessLayer.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DataAccessLayer.Repositories.GenericRepo
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class , BaseEntity
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
        //public async Task<T?> GetByIdAsync(int id)
        //{
        //    return await context.Set<T>().AsNoTracking().FindAsync(id);
        //}
        public async Task<T?> GetByIdAsync(int id)
        {
            return await context.Set<T>().AsNoTracking().FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        }
        public async Task AddAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
        }

        //Attach: Attaches the entity to the context.
        //This means the entity is now being tracked by the context,
        //but no changes are yet made.
        //Attach: You use this method when you know the entity already
        //exists in the database, and you just want to track it without
        //making any changes. For example, this is useful when you're working
        //with entities that were previously
        //loaded from the database but are no longer tracked.
        public void Update(T entity, params string[] properties)
        {
            // dbEntity is object entity . 
            var dbEntity = context.Set<T>().Local.FirstOrDefault(e => e.Id == entity.Id);
            EntityEntry entryEntry;
            if (dbEntity == null)
            {
                 entryEntry = context.Set<T>().Attach(entity);  // Attach the entity to the context , the entity’s state will typically be Unchanged.
            }
            else {
                entryEntry = context.ChangeTracker.Entries<T>().Where
                    (e => e.Entity.Id == entity.Id).FirstOrDefault(); 
            }
           

            foreach (var property in entryEntry.Properties)
            {
                if (properties.Contains(property.Metadata.Name))
                {
                    property.CurrentValue = entity.GetType().GetProperty(property.Metadata.Name).GetValue(entity);
                    property.IsModified = true;
                }
            }
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
