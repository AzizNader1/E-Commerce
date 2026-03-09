
using E_Commerce.API.Data;

namespace E_Commerce.API.Repositories
{
    /// <summary>
    /// This class represents a generic repository that provides basic CRUD (Create, Read, Update, Delete) operations for any entity type. It uses the ApplicationDBContext to interact with the database and perform these operations. The GenericRepository class is designed to be reusable and can be used for any entity type that is a class. It provides methods to add, delete, retrieve all, retrieve by ID, and update entities in the database. This approach promotes code reusability and separation of concerns by abstracting the data access logic from the business logic of the application.
    /// </summary>
    /// <remarks>
    /// The GenericRepository class is a common design pattern used in software development to provide a centralized and reusable way to perform CRUD operations on different entity types. By using a generic type parameter (T), this class can work with any entity class, allowing for flexibility and reducing code duplication. The methods in this class interact with the database context to perform the necessary operations, making it easier to manage data access across the application. This design promotes maintainability and scalability by allowing developers to easily add new entity types without having to write separate repository classes for each one.
    /// </remarks>
    /// <typeparam name="T"></typeparam>
    /// <return>
    /// An instance of the GenericRepository class that can be used to perform CRUD operations on any entity type that is a class. The methods provided by this class allow for adding, deleting, retrieving, and updating entities in the database using the ApplicationDBContext. This generic repository pattern helps to abstract away the data access logic and promotes code reusability across different parts of the application.
    /// </return>
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
