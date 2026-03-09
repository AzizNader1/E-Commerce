using E_Commerce.API.DTOs.CategoryDTOs;

namespace E_Commerce.API.Services
{
    /// <summary>
    /// This interface defines the contract for the category service in an e-commerce application. It includes methods for retrieving all categories, retrieving a category by its ID or name, adding a new category, updating an existing category, and deleting a category. Each method is designed to handle specific operations related to product categories, ensuring that the application can manage categories effectively. The service is responsible for implementing the business logic associated with categories and interacting with the data layer to perform CRUD operations on category data.
    /// </summary>
    public interface ICategoryService
    {
        List<CategoryDto> GetAllCategories();
        CategoryDto GetCategoryById(int categoryId);
        CategoryDto GetCategoryByName(string categoryName);
        void AddCategory(CreateCategoryDto createCategoryDto);
        void UpdateCategory(CategoryDto categoryDto);
        void DeleteCategory(int categoryId);
    }
}
