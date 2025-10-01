using System.ComponentModel.DataAnnotations;

namespace E_Commerce.API.DTOs.OrderDTOs
{
    public class OrderDto
    {
        public int OrderId { get; set; }

        public int UserId { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public string Status { get; set; }

        public decimal TotalAmount { get; set; }
    }
}
