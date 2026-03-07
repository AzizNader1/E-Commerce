using E_Commerce.MVC.Models;

namespace E_Commerce.MVC.DTOs.ProductDTOs
{
    public class ProductDto
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public string ProductDescription { get; set; } = string.Empty;

        public decimal ProductPrice { get; set; }

        public int ProductStockQuantity { get; set; }

        public string? ProductImage { get; set; }

        public string? ProductImageContentType { get; set; }

        public bool IsProductHasImage => !string.IsNullOrEmpty(ProductImage);

        public int CategoryId { get; set; }

        public CategoriesCollections? CategoryName { get; set; }
    }
}
