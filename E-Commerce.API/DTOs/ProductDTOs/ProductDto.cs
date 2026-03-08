using E_Commerce.API.Models;

namespace E_Commerce.API.DTOs.ProductDTOs
{
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
