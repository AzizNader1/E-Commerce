namespace E_Commerce.API.Models
{
    public enum CategoriesCollections
    {
        Electronics = 1,
        Clothing = 2,
        HomeAppliances = 3,
        Books = 4,
        BeautyProducts = 5,
        SportsEquipment = 6,
        ToysAndGames = 7,
        AutomotiveParts = 8,
        HealthAndPersonalCare = 9,
        OfficeSupplies = 10
    }
    public class Category
    {
        public int CategoryId { get; set; }
        public CategoriesCollections? CategoryName { get; set; }
        public string? CategoryDescription { get; set; }


        // Navigation properties
        public virtual ICollection<Product> Products { get; set; } = [];
    }
}
