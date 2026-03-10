using E_Commerce.MVC.Models;

namespace E_Commerce.MVC.DTOs.CategoryDTOs
{
    /// <summary>
    /// This class represents a data transfer object (DTO) for a category in an e-commerce application. It contains properties to hold the category's unique identifier (CategoryId), the name of the category (CategoryName), and a description of the category (CategoryDescription). This DTO is used to transfer category data between different layers of the application, such as between the API and the service layer or between the service layer and the database.
    /// </summary>
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public CategoriesCollections? CategoryName { get; set; }
        public string CategoryDescription { get; set; } = string.Empty;
    }
}
