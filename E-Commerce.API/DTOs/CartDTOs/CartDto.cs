using E_Commerce.API.DTOs.CartItemDTOs;

namespace E_Commerce.API.DTOs.CartDTOs
{
    public class CartDto
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public List<CartItemDto> CartItems { get; set; }
    }
}
