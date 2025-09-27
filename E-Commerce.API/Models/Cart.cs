using System.ComponentModel.DataAnnotations;

namespace E_Commerce.API.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        [Required]
        public int UserId { get; set; } // Foreign Key to User

        // Relationships
        public virtual User User { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>(); // One-to-Many
    }
}
