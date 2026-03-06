using E_Commerce.API.DTOs.OrderItemDTOs;

namespace E_Commerce.API.Services
{
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
