using E_Commerce.API.DTOs.OrderDTOs;

namespace E_Commerce.API.Services
{
    /// <summary>
    /// This interface defines the contract for the order service in an e-commerce application. It includes methods for retrieving all orders, retrieving an order by its ID, retrieving orders by user ID or user name, adding a new order, updating an existing order, and deleting an order. Each method is designed to handle specific operations related to orders, ensuring that the application can manage users' orders effectively. The service is responsible for implementing the business logic associated with orders and interacting with the data layer to perform CRUD operations on order data.
    /// </summary>
    public interface IOrderService
    {
        List<OrderDto> GetAllOrders();
        OrderDto GetOrderById(int orderId);
        List<OrderDto> GetAllOrdersByUserId(int userId);
        List<OrderDto> GetAllOrdersByUserName(string userName);
        void AddOrder(CreateOrderDto createOrderDto);
        void UpdateOrder(OrderDto orderDto);
        void DeleteOrder(int orderId);
    }
}
