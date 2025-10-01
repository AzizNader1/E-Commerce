namespace E_Commerce.API.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        List<T> GetAllAsync();
        T GetByIdAsync(int id);
        void AddAsync(T entity);
        void UpdateAsync(T entity);
        void DeleteAsync(int id);
    }
}
