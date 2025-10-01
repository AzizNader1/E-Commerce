using E_Commerce.API.DTOs.CartItemDTOs;

namespace E_Commerce.API.Services
{
    public interface ICartItemService
    {
        List<CartItemDto> GetAllCartItemsAsync();
        CartItemDto GetCartItemByIdAsync(int cartItemId);
        List<CartItemDto> GetCartItemsByCartIdAsync(int cartId);
        void AddCartItemAsync(CreateCartItemDto ceateCartItemDto);
        void UpdateCartItemAsync(CartItemDto cartItemDto);
        void DeleteCartItemAsync(int cartItemId);
    }
}
