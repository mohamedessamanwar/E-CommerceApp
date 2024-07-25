namespace DataAccessLayer.Repositories.GenericRepo
{
    public interface IGenericRepo<T> where T : class // primary constrain 
    {
        Task<IEnumerable<T>> GetAll();
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
