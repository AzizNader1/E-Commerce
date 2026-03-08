using E_Commerce.API.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.API.Data
{
    /// <summary>
    /// This class represents the database context for an e-commerce application. It inherits from the DbContext class provided by Entity Framework Core and is used to interact with the underlying database. The ApplicationDBContext class defines DbSet properties for each of the entities in the application, such as Users, Products, Categories, CartItems, Carts, Orders, and OrderItems. These DbSet properties allow for querying and saving instances of these entities to the database. The constructor of the ApplicationDBContext class takes DbContextOptions as a parameter, which is used to configure the database connection and other options for the context.
    /// </summary>
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

    }
}
