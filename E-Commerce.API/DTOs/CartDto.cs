using System.ComponentModel.DataAnnotations;

namespace E_Commerce.API.DTOs
{
    public class CartDto
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
    }
}
