namespace E_Commerce.MVC.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }


        // Navigation properties
        public virtual Cart? Cart { get; set; }
        public virtual Product? Product { get; set; }
    }
}
