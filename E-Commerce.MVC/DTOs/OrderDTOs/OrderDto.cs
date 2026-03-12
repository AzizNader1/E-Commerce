using E_Commerce.MVC.DTOs.OrderItemDTOs;
using E_Commerce.MVC.Models;

namespace E_Commerce.MVC.DTOs.OrderDTOs
{
    /// <summary>
    /// This class represents a data transfer object (DTO) for an order in an e-commerce application. It contains properties to hold the unique identifier of the order (OrderId), the user ID associated with the order (UserId), the date of the order (OrderDate), the status of the order (Status), the total amount of the order (TotalAmount), and a list of order items associated with the order (OrderItems). This DTO is used to transfer order data between different layers of the application, such as between the API and the service layer or between the service layer and the database. The OrderItems property is initialized as an empty list to ensure that it can hold multiple order items when necessary.
    /// </summary>
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
