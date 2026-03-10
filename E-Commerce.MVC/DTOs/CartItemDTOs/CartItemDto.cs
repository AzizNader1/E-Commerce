namespace E_Commerce.MVC.DTOs.CartItemDTOs
{
    /// <summary>
    /// This class represents a data transfer object (DTO) for a cart item in an e-commerce application.
    /// </summary>
    public class CartItemDto
    {
        public int CartItemId { get; set; }

        public int CartId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
