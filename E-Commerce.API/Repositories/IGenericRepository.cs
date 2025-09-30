namespace E_Commerce.API.Repositories
{
    public interface IGenericRepository
    {
        List<T> GetAllAsync<T>() where T : class;
        T GetByIdAsync<T>(int id) where T : class;
        void AddAsync<T>(T entity) where T : class;
        void UpdateAsync<T>(T entity) where T : class;
        void DeleteAsync<T>(int id) where T : class;
    }
}
