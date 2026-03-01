using System.ComponentModel.DataAnnotations;

namespace E_Commerce.API.DTOs.ProductDTOs
{
    public class ProductDto
    {
        public int ProductId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        public string? ImageBase64 { get; set; }

        public string? ImageContentType { get; set; }

        public bool HasImage => !string.IsNullOrEmpty(ImageBase64);

        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = string.Empty;
    }
}
