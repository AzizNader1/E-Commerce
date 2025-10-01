using System.ComponentModel.DataAnnotations;

namespace E_Commerce.MVC.Models
{
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }

        [Required]
        public int OrderId { get; set; } 

        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal UnitPrice { get; set; } 

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
