using E_Commerce.MVC.Models;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.MVC.DTOs.OrderDTOs
{
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