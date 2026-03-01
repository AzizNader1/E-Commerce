using E_Commerce.API.DTOs.ProductDTOs;

namespace E_Commerce.API.Services
{
    public interface IProductService
    {
        List<ProductDto> GetAllProductsAsync();
        ProductDto GetProductByIdAsync(int productId);
        List<ProductDto> GetAllProductsByCategoryId(int categoryId);
        List<ProductDto> GetAllProductsByCategoryName(string categoryName);
        ProductDto AddProductAsync(CreateProductDto createProductDto, IFormFile productImage);
        ProductDto UpdateProductAsync(UpdateProductDto updateProductDto, IFormFile productImage);
        void DeleteProductAsync(int productId);
    }
}
