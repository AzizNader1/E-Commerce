
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
        public void AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void DeleteAsync(int id)
        {
            _context.Set<T>().Remove(_context.Set<T>().Find(id)!);
            _context.SaveChanges();
        }

        public List<T> GetAllAsync()
        {
            return _context.Set<T>().ToList()!;
        }

        public T GetByIdAsync(int id)
        {
           return _context.Set<T>().Find(id)!;
        }

        public void UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }
    }
}
