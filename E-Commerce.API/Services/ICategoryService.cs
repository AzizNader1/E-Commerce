using E_Commerce.API.DTOs.CategoryDTOs;

namespace E_Commerce.API.Services
{
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
