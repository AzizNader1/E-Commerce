using E_Commerce.API.DTOs;

namespace E_Commerce.API.Services
{
    public interface IOrderService
    {
        List<OrderDto> GetAllAsync();
        OrderDto GetByIdAsync(int id);
        void AddAsync(OrderDto orderDto);
        void UpdateAsync(OrderDto orderDto);
        void DeleteAsync(int id);
    }
}
