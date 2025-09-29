using System.ComponentModel.DataAnnotations;

namespace E_Commerce.API.Models
{
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; }

        [Required]
        public int CartId { get; set; } 

        [Required]
        public int ProductId { get; set; } 

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        public virtual Cart Cart { get; set; }
        public virtual Product Product { get; set; }
    }
}
