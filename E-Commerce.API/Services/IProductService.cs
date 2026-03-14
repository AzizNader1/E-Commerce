using E_Commerce.API.DTOs.ProductDTOs;

namespace E_Commerce.API.Services
{
    /// <summary>
    /// This interface defines the contract for the product service in an e-commerce application. It includes methods for retrieving all products, retrieving a product by its ID, retrieving products by category ID or category name, adding a new product, updating an existing product, and deleting a product. Each method is designed to handle specific operations related to products, ensuring that the application can manage its product catalog effectively. The service is responsible for implementing the business logic associated with products and interacting with the data layer to perform CRUD operations on product data.
    /// </summary>
    public interface IProductService
    {
        List<ProductDto> GetAllProducts();
        ProductDto GetProductById(int productId);
        List<ProductDto> GetAllProductsByCategoryId(int categoryId);
        List<ProductDto> GetAllProductsByCategoryName(string categoryName);
        ProductDto AddProduct(CreateProductDto createProductDto, IFormFile productImage);
        ProductDto UpdateProduct(UpdateProductDto updateProductDto, IFormFile productImage);
        ProductDto UpdateProductQuantity(int productId, int qunatity);
        void DeleteProduct(int productId);
    }
}
