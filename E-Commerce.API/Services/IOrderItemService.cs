using E_Commerce.API.DTOs.OrderItemDTOs;

namespace E_Commerce.API.Services
{
    public interface IOrderItemService
    {
        List<OrderItemDto> GetAllOrderItemsAsync();
        OrderItemDto GetOrderItemByIdAsync(int orderItemId);
        List<OrderItemDto> GetOrderItemsByOrderIdAsync(int orderId);
        void AddOrderItemAsync(CreateOrderItemDto createOrderItemDto);
        void UpdateOrderItemAsync(OrderItemDto orderItemDto);
        void DeleteOrderItemAsync(int orderItemId);
    }
}
