using E_Commerce.MVC.DTOs.CategoryDTOs;

namespace E_Commerce.MVC.Services
{
    public interface IApiCategoriesService
    {
        Task<List<CategoryDto>> GetAllCategoriesAsync();
        Task<CategoryDto?> GetCategoryByIdAsync(int id);

        Task<bool> CreateCategoryAsync(CreateCategoryDto dto);
        Task<bool> UpdateCategoryAsync(CategoryDto dto);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
