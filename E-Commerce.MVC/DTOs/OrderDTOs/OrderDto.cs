using E_Commerce.MVC.DTOs.OrderItemDTOs;
using E_Commerce.MVC.Models;

namespace E_Commerce.MVC.DTOs.OrderDTOs
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
