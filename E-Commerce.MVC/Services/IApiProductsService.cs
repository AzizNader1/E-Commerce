using E_Commerce.MVC.DTOs.ProductDTOs;

namespace E_Commerce.MVC.Services
{
    public interface IApiProductsService
    {
        Task<List<ProductDto>> GetAllProductsAsync();
        Task<ProductDto?> GetProductByIdAsync(int id);

        // Admin operations
        Task<ProductDto?> CreateProductAsync(CreateProductDto dto, IFormFile? productImage = null);
        Task<ProductDto?> UpdateProductAsync(UpdateProductDto dto, IFormFile? productImage = null);
        Task<bool> DeleteProductAsync(int id);
    }
}
