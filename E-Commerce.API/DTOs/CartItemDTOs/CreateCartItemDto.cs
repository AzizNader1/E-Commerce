namespace E_Commerce.API.DTOs.CartItemDTOs
{
    /// <summary>
    /// This class represents a data transfer object (DTO) for creating a cart item in an e-commerce application.
    /// </summary>
    public class CreateCartItemDto
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
