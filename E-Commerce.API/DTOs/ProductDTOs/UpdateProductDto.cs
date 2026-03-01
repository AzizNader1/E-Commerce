using System.ComponentModel.DataAnnotations;

namespace E_Commerce.API.DTOs.ProductDTOs
{
    public class UpdateProductDto
    {

        [Required]
        public int ProductId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public bool UpdateImage { get; set; } = false;
    }
}
