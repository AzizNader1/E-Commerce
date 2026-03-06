using E_Commerce.API.DTOs.ProductDTOs;

namespace E_Commerce.API.Services
{
    public interface IProductService
    {
        List<ProductDto> GetAllProducts();
        ProductDto GetProductById(int productId);
        List<ProductDto> GetAllProductsByCategoryId(int categoryId);
        List<ProductDto> GetAllProductsByCategoryName(string categoryName);
        ProductDto AddProduct(CreateProductDto createProductDto, IFormFile productImage);
        ProductDto UpdateProduct(UpdateProductDto updateProductDto, IFormFile productImage);
        void DeleteProduct(int productId);
    }
}
