using E_Commerce.MVC.DTOs.OrderDTOs;
using E_Commerce.MVC.Models;

namespace E_Commerce.MVC.Services
{
    public interface IApiOrdersService
    {
        Task<List<OrderProductDto>> GetAllOrdersAsync();
        Task<OrderProductDto?> GetOrderByIdAsync(int orderId);
        Task<List<OrderProductDto>> GetOrdersByUserIdAsync(int userId);
        Task<List<OrderProductDto>> GetOrdersByUserNameAsync(string userName);
        Task<OrderProductDto?> CreateOrderAsync(CreateOrderDto dto);
        Task<bool> UpdateOrderStatusAsync(int orderId, OrderStatus status);
        Task<bool> DeleteOrderAsync(int orderId);
    }
}
