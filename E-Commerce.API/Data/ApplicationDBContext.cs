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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    CategoryId = 1,
                    CategoryName = CategoriesCollections.Electronics,
                    CategoryDescription = "Electronic devices, gadgets, and accessories including smartphones, laptops, tablets, cameras, and audio equipment."
                },
                new Category
                {
                    CategoryId = 2,
                    CategoryName = CategoriesCollections.Clothing,
                    CategoryDescription = "Apparel and fashion items for men, women, and children including shirts, pants, dresses, shoes, and accessories."
                },
                new Category
                {
                    CategoryId = 3,
                    CategoryName = CategoriesCollections.HomeAppliances,
                    CategoryDescription = "Household appliances and equipment for daily use including refrigerators, washing machines, vacuum cleaners, and kitchen appliances."
                },
                new Category
                {
                    CategoryId = 4,
                    CategoryName = CategoriesCollections.Books,
                    CategoryDescription = "Printed and digital books covering various genres including fiction, non-fiction, educational, self-help, and children's literature."
                },
                new Category
                {
                    CategoryId = 5,
                    CategoryName = CategoriesCollections.BeautyProducts,
                    CategoryDescription = "Cosmetics, skincare, haircare, and personal grooming products including makeup, lotions, shampoos, and fragrances."
                },
                new Category
                {
                    CategoryId = 6,
                    CategoryName = CategoriesCollections.SportsEquipment,
                    CategoryDescription = "Sports and fitness equipment including gym gear, outdoor sports items, athletic wear, and exercise accessories."
                },
                new Category
                {
                    CategoryId = 7,
                    CategoryName = CategoriesCollections.ToysAndGames,
                    CategoryDescription = "Toys, games, and entertainment products for all ages including action figures, board games, puzzles, and video games."
                },
                new Category
                {
                    CategoryId = 8,
                    CategoryName = CategoriesCollections.AutomotiveParts,
                    CategoryDescription = "Automotive parts, accessories, and tools for vehicle maintenance and customization including tires, batteries, and car care products."
                },
                new Category
                {
                    CategoryId = 9,
                    CategoryName = CategoriesCollections.HealthAndPersonalCare,
                    CategoryDescription = "Health and wellness products including vitamins, supplements, first aid supplies, and personal hygiene items."
                },
                new Category
                {
                    CategoryId = 10,
                    CategoryName = CategoriesCollections.OfficeSupplies,
                    CategoryDescription = "Office equipment and supplies including stationery, furniture, organizers, and technology accessories for workplace productivity."
                }
            );
        }

    }
}
