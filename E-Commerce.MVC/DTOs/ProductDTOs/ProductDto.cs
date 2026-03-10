using E_Commerce.MVC.Models;

namespace E_Commerce.MVC.DTOs.ProductDTOs
{
    /// <summary>
    /// This class represents a data transfer object (DTO) for a product in an e-commerce application. It contains properties to hold the unique identifier of the product (ProductId), the name of the product (ProductName), a description of the product (ProductDescription), the price of the product (ProductPrice), the stock quantity of the product (ProductStockQuantity), an optional image of the product (ProductImage) along with its content type (ProductImageContentType), a boolean flag indicating whether the product has an image (IsProductHasImage), and the category ID to which the product belongs (CategoryId) along with an optional reference to the category name (CategoryName). This DTO is used to transfer product data between different layers of the application, such as between the API and the service layer or between the service layer and the database.
    /// </summary>
    public class ProductDto
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public string ProductDescription { get; set; } = string.Empty;

        public decimal ProductPrice { get; set; }

        public int ProductStockQuantity { get; set; }

        public byte[]? ProductImage { get; set; }

        public string? ProductImageContentType { get; set; }

        public bool IsProductHasImage { get; set; } = false;

        public int CategoryId { get; set; }

        public CategoriesCollections? CategoryName { get; set; }
    }
}
