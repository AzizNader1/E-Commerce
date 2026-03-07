using System.ComponentModel.DataAnnotations;

namespace E_Commerce.MVC.DTOs.OrderItemDTOs
{
    public class CreateOrderItemDto
    {
        [Required(ErrorMessage = "Order Id Is Required Field And You Can Not Left It Empty")]
        [Range(1, int.MaxValue,
            ErrorMessage = "Order Id Must Be A Valid Positive Number")]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Product Id Is Required Field And You Can Not Left It Empty")]
        [Range(1, int.MaxValue,
            ErrorMessage = "Product Id Must Be A Valid Positive Number")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Quantity Is Required Field And You Can Not Left It Empty")]
        [Range(1, 1000,
            ErrorMessage = "Quantity Must Be Between 1 And 1000")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Unit Price Is Required Field And You Can Not Left It Empty")]
        [Range(0.01, 1000000,
            ErrorMessage = "Unit Price Must Be Between 0.01 And 1000000")]
        public decimal UnitPrice { get; set; }
    }
}