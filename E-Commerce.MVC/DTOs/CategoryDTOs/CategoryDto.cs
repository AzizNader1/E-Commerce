using E_Commerce.MVC.Models;

namespace E_Commerce.MVC.DTOs.CategoryDTOs
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public CategoriesCollections? CategoryName { get; set; }
        public string CategoryDescription { get; set; } = string.Empty;
    }
}
