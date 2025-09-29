using System.ComponentModel.DataAnnotations;

namespace E_Commerce.API.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        [Required]
        public int UserId { get; set; } 

       
        public virtual User User { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>(); 
    }
}
