using System.ComponentModel.DataAnnotations;

namespace E_Commerce.API.Models
{
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }

        [Required]
        public int OrderId { get; set; } // Foreign Key to Order

        [Required]
        public int ProductId { get; set; } // Foreign Key to Product

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal UnitPrice { get; set; } // Price at the time of order

        // Relationships
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
