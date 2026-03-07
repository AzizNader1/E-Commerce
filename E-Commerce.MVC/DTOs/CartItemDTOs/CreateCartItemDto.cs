namespace E_Commerce.MVC.DTOs.CartItemDTOs
{
    public class CreateCartItemDto
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
