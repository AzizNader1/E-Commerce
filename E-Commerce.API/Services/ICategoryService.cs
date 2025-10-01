using E_Commerce.API.DTOs.CategoryDTOs;

namespace E_Commerce.API.Services
{
    public interface ICategoryService
    {
        List<CategoryDto> GetAllCategoriesAsync();
        CategoryDto GetCategoryByIdAsync(int categoryId);
        void AddCategoryAsync(CreateCategoryDto createCategoryDto);
        void UpdateCategoryAsync(CategoryDto categoryDto);
        void DeleteCategoryAsync(int categoryId);
    }
}
