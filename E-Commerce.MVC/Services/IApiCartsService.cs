using E_Commerce.MVC.DTOs.CartDTOs;
using E_Commerce.MVC.DTOs.CartItemDTOs;

namespace E_Commerce.MVC.Services
{
    public interface IApiCartsService
    {
        Task<CartShoppingDto> GetCartByUserNameAsync(string userName);
        Task<CartShoppingDto> GetCartByIdAsync(int cartId);
        Task<CartWithProductDto> AddCartItemAsync(CreateCartItemDto dto);
        Task<CartWithProductDto> UpdateCartItemAsync(CartItemDto dto);
        Task<bool> DeleteCartItemAsync(int cartItemId);
        Task<bool> ClearCartAsync(int cartId);
        Task<bool> GetCartByUserName(string userName);
    }
}
