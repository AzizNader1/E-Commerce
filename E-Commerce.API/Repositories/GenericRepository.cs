
using E_Commerce.API.Data;

namespace E_Commerce.API.Repositories
{
    public class GenericRepository : IGenericRepository
    {
        private readonly ApplicationDBContext _context;
        public GenericRepository(ApplicationDBContext context) 
        { 
            _context = context;
        }
        public void AddAsync<T>(T entity) where T : class
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void DeleteAsync<T>(int id) where T : class
        {
            _context.Set<T>().Remove(_context.Set<T>().Find(id)!);
            _context.SaveChanges();
        }

        public List<T> GetAllAsync<T>() where T : class
        {
            return _context.Set<T>().ToList()!;
        }

        public T GetByIdAsync<T>(int id) where T : class
        {
           return _context.Set<T>().Find(id)!;
        }

        public void UpdateAsync<T>(T entity) where T : class
        {
            _context.Set<T>().Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
