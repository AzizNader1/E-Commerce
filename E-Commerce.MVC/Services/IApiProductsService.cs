using E_Commerce.MVC.DTOs.ProductDTOs;

namespace E_Commerce.MVC.Services
{
    public interface IApiProductsService
    {
        Task<List<ProductDto>> GetAllProductsAsync();
        Task<ProductDto?> GetProductByIdAsync(int id);
    }
}
