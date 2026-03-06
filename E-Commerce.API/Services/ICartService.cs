using E_Commerce.API.DTOs.CartDTOs;

namespace E_Commerce.API.Services
{
    public interface ICartService
    {
        List<CartDto> GetAllCarts();
        CartDto GetCartById(int cartId);
        List<CartDto> GetAllCartsByUserId(int userId);
        List<CartDto> GetAllCartsByUserName(string userName);
        void AddCart(CreateCartDto ceateCartDto);
        void UpdateCart(CartDto cartDto);
        void DeleteCart(int cartId);
    }
}
