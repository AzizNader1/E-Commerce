namespace E_Commerce.MVC.DTOs.OrderItemDTOs
{
    /// <summary>
    /// This class represents a data transfer object (DTO) for an order item in an e-commerce application. It contains properties to hold the order item ID (OrderItemId), the order ID associated with the order item (OrderId), the product ID of the item being ordered (ProductId), the quantity of the product being ordered (Quantity), and the unit price of the product at the time of the order (UnitPrice). This DTO is used to transfer order item data between different layers of the application, such as from the database to the client or vice versa.
    /// </summary>
    public class OrderItemDto
    {
        public int OrderItemId { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
