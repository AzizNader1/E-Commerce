using E_Commerce.API.DTOs;

namespace E_Commerce.API.Services
{
    public interface IOrderItemService
    {
        List<OrderItemDto> GetAllAsync();
        OrderItemDto GetByIdAsync(int id);
        void AddAsync(OrderItemDto orderItemDto);
        void UpdateAsync(OrderItemDto orderItemDto);
        void DeleteAsync(int id);
    }
}
