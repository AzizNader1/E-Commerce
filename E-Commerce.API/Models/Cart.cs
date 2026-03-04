namespace E_Commerce.API.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public int UserId { get; set; }


        // Navigation properties
        public virtual User? User { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; } = [];
    }
}
