using System.ComponentModel.DataAnnotations;

namespace E_Commerce.API.DTOs.CartDTOs
{
    public class CartDto
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
    }
}
