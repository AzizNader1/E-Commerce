using E_Commerce.MVC.DTOs.CheckoutDTOs;
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
        Task<OrderProductDto?> CreateOrderAsync(CheckoutViewModel checkoutViewModel);
        Task<bool> UpdateOrderStatusAsync(int orderId, OrderStatus status);
        Task<bool> CancleOrRejectAndRevertQuantity(int orderId, OrderStatus status);
        Task<bool> DeleteOrderAsync(int orderId);

        Task<CheckoutViewModel?> GetSingleProductCheckoutAsync(string userName, int productId, int quantity = 1);
        Task<CartCheckoutViewModel?> GetCartCheckoutAsync(string userName, string selectedItems);
        Task<(bool Success, string Message, int ItemsCount)> PlaceOrderFromCartAsync(string userName, string selectedItems);
    }
}
