namespace E_Commerce.MVC.DTOs.CartDTOs
{
    public class CartShoppingDto
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public List<CartWithProductDto> CartItems { get; set; } = new List<CartWithProductDto>();

        // Computed properties for display
        public decimal TotalPrice => CartItems?.Sum(item => item.TotalPrice) ?? 0;
        public int TotalItems => CartItems?.Sum(item => item.Quantity) ?? 0;
    }
}
