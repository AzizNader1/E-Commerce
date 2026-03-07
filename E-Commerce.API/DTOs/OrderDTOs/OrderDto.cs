using E_Commerce.API.DTOs.OrderItemDTOs;
using E_Commerce.API.Models;

namespace E_Commerce.API.DTOs.OrderDTOs
{
    public class OrderDto
    {
        public int OrderId { get; set; }

        public int UserId { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public OrderStatus Status { get; set; }

        public decimal TotalAmount { get; set; }

        public List<OrderItemDto> OrderItems { get; set; } = [];
    }
}
