using System.ComponentModel.DataAnnotations;

namespace E_Commerce.API.DTOs.OrderItemDTOs
{
    public class OrderItemDto
    {
        public int OrderItemId { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
