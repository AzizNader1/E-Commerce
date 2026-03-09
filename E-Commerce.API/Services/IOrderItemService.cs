using E_Commerce.API.DTOs.OrderItemDTOs;

namespace E_Commerce.API.Services
{
    /// <summary>
    /// This interface defines the contract for the order item service in an e-commerce application. It includes methods for retrieving all order items, retrieving an order item by its ID, retrieving order items by order ID, adding a new order item, updating an existing order item, and deleting an order item. Each method is designed to handle specific operations related to order items, ensuring that the application can manage the items in users' orders effectively. The service is responsible for implementing the business logic associated with order items and interacting with the data layer to perform CRUD operations on order item data.
    /// </summary>
    public interface IOrderItemService
    {
        List<OrderItemDto> GetAllOrderItems();
        OrderItemDto GetOrderItemById(int orderItemId);
        List<OrderItemDto> GetOrderItemsByOrderId(int orderId);
        void AddOrderItem(CreateOrderItemDto createOrderItemDto);
        void UpdateOrderItem(OrderItemDto orderItemDto);
        void DeleteOrderItem(int orderItemId);
    }
}
