namespace DataAccessLayer.Repositories.GenericRepo
{
    public interface IGenericRepo<T> where T : class // primary constrain 
    {
        IEnumerable<T> GetAll();
        T? GetById(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
