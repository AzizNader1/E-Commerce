using E_Commerce.API.Models;

namespace E_Commerce.API.DTOs.CategoryDTOs
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public CategoriesCollections? CategoryName { get; set; }
        public string CategoryDescription { get; set; } = string.Empty;
    }
}
