using E_Commerce.API.DTOs;

namespace E_Commerce.API.Services
{
    public interface ICartItemService
    {
        List<CartItemDto> GetAllAsync();
        CartItemDto GetByIdAsync(int id);
        void AddAsync(CartItemDto cartItemDto);
        void UpdateAsync(CartItemDto cartItemDto);
        void DeleteAsync(int id);
    }
}
