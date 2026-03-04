namespace E_Commerce.API.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductStockQuantity { get; set; }
        public byte[]? ProductImageData { get; set; }
        public string? ProductImageContentType { get; set; }
        public int CategoryId { get; set; }


        // Navigation properties
        public virtual Category? Category { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; } = [];
        public virtual ICollection<OrderItem> OrderItems { get; set; } = [];
    }
}
