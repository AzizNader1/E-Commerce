using E_Commerce.API.DTOs;

namespace E_Commerce.API.Services
{
    public interface IProductService
    {
        List<ProductDto> GetAllAsync();
        ProductDto GetByIdAsync(int id);
        void AddAsync(ProductDto productDto);
        void UpdateAsync(ProductDto productDto);
        void DeleteAsync(int id);
    }
}
