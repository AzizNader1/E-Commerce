using System.ComponentModel.DataAnnotations;

namespace E_Commerce.API.DTOs.CartItemDTOs
{
    public class CartItemDto
    {
        public int CartItemId { get; set; }

        public int CartId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
