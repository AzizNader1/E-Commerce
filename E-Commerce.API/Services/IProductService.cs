using E_Commerce.API.DTOs.ProductDTOs;

namespace E_Commerce.API.Services
{
    public interface IProductService
    {
        List<ProductDto> GetAllProductsAsync();
        ProductDto GetProductByIdAsync(int productId);
        List<ProductDto> GetAllProductsByCategoryId(int categoryId);
        List<ProductDto> GetAllProductsByCategoryName(string categoryName);
        void AddProductAsync(CreateProductDto createProductDto);
        void UpdateProductAsync(ProductDto productDto);
        void DeleteProductAsync(int productId);
    }
}
