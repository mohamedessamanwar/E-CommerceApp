namespace DataAccessLayer.Repositories.GenericRepo
{
    public interface IGenericRepo<T> where T : class // primary constrain 
    {
        Task<IEnumerable<T>> GetAll();
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity, params string[] properties);
        void Delete(T entity);
        void DeleteBulk(IEnumerable<T> entities);
    }
}
