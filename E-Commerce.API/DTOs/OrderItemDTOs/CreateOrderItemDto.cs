using System.ComponentModel.DataAnnotations;

namespace E_Commerce.API.DTOs.OrderItemDTOs
{
    /// <summary>
    /// This class represents a data transfer object (DTO) for creating a new order item in an e-commerce application. It contains properties to hold the order ID associated with the order item (OrderId), the product ID of the item being ordered (ProductId), the quantity of the product being ordered (Quantity), and the unit price of the product at the time of the order (UnitPrice). The OrderId and ProductId properties are marked as required and must be valid positive numbers. The Quantity property is also required and must be between 1 and 1000. The UnitPrice property is required and must be a decimal value between 0.01 and 1,000,000. This DTO is used to transfer order item data from the client to the server when creating a new order item in the system.
    /// </summary>
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