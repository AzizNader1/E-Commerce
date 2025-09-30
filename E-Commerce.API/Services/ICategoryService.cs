using E_Commerce.API.DTOs;

namespace E_Commerce.API.Services
{
    public interface ICategoryService
    {
        List<CategoryDto> GetAllAsync();
        CategoryDto GetByIdAsync(int id);
        void AddAsync(CategoryDto categoryDto);
        void UpdateAsync(CategoryDto categoryDto);
        void DeleteAsync(int id);
    }
}
