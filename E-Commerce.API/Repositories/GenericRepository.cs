
using E_Commerce.API.Data;

namespace E_Commerce.API.Repositories
{
    public class GenericRepository<T> where T : class
    {
        private readonly ApplicationDBContext _context;
        public GenericRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public void AddModel(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void DeleteModel(int id)
        {
            _context.Set<T>().Remove(_context.Set<T>().Find(id)!);
            _context.SaveChanges();
        }

        public List<T> GetAllModels()
        {
            return _context.Set<T>().ToList()!;
        }

        public T GetModelById(int id)
        {
            return _context.Set<T>().Find(id)!;
        }

        public void UpdateModel(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }
    }
}
