using E_Commerce.API.DTOs.OrderDTOs;

namespace E_Commerce.API.Services
{
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
