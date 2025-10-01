using E_Commerce.API.DTOs.CartDTOs;

namespace E_Commerce.API.Services
{
    public interface ICartService
    {
        List<CartDto> GetAllCartsAsync();
        CartDto GetCartByIdAsync(int cartId);
        List<CartDto> GetAllCartsByUserIdAsync(int userId);
        List<CartDto> GetAllCartsByUserNameAsync(string userName);
        void AddCartAsync(CreateCartDto ceateCartDto);
        void UpdateCartAsync(CartDto cartDto);
        void DeleteCartAsync(int cartId);
    }
}
