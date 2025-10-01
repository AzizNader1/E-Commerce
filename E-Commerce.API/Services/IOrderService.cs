using E_Commerce.API.DTOs.OrderDTOs;

namespace E_Commerce.API.Services
{
    public interface IOrderService
    {
        List<OrderDto> GetAllOrdersAsync();
        OrderDto GetOrderByIdAsync(int orderId);
        List<OrderDto> GetAllOrdersByUserId(int userId);
        List<OrderDto> GetAllOrdersByUserName(string userName);
        void AddOrderAsync(CreateOrderDto createOrderDto);
        void UpdateOrderAsync(OrderDto orderDto);
        void DeleteOrderAsync(int orderId);
    }
}
