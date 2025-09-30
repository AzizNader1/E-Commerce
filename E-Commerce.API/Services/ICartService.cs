using E_Commerce.API.DTOs;

namespace E_Commerce.API.Services
{
    public interface ICartService
    {
        List<CartDto> GetAllAsync();
        CartDto GetByIdAsync(int id);
        void AddAsync(CartDto cartDto);
        void UpdateAsync(CartDto cartDto);
        void DeleteAsync(int id);
    }
}
