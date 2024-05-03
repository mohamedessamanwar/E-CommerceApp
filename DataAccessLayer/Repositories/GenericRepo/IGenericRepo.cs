namespace DataAccessLayer.Repositories.GenericRepo
{
    public interface IGenericRepo<T> where T : class // primary constrain 
    {
        IEnumerable<T> GetAll();
        T? GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
