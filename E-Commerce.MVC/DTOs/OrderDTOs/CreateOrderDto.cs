using E_Commerce.MVC.Models;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.MVC.DTOs.OrderDTOs
{
    /// <summary>
    /// This class represents a data transfer object (DTO) for creating a new order in an e-commerce application. It contains properties to hold the user ID associated with the order (UserId), the date of the order (OrderDate), the status of the order (Status), and the total amount of the order (TotalAmount). The UserId property is marked as required and must be a valid positive number. The OrderDate property is also required and defaults to the current UTC date and time. The Status property is required and represents the current status of the order. The TotalAmount property is required and must be a decimal value between 0.01 and 1,000,000. This DTO is used to transfer order data from the client to the server when creating a new order in the system.
    /// </summary>
    public class CreateOrderDto
    {
        [Required(ErrorMessage = "User Id Is Required Field And You Can Not Left It Empty")]
        [Range(1, int.MaxValue,
            ErrorMessage = "User Id Must Be A Valid Positive Number")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Order Date Is Required Field And You Can Not Left It Empty")]
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "Order Status Is Required Field And You Can Not Left It Empty")]
        public OrderStatus Status { get; set; }

        [Required(ErrorMessage = "Total Amount Is Required Field And You Can Not Left It Empty")]
        [Range(0.01, 1000000,
            ErrorMessage = "Total Amount Must Be Between 0.01 And 1000000")]
        public decimal TotalAmount { get; set; }
    }
}