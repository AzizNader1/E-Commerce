using E_Commerce.API.DTOs.CartItemDTOs;

namespace E_Commerce.API.Services
{
    public interface ICartItemService
    {
        List<CartItemDto> GetAllCartItems();
        CartItemDto GetCartItemById(int cartItemId);
        List<CartItemDto> GetCartItemsByCartId(int cartId);
        void AddCartItem(CreateCartItemDto ceateCartItemDto);
        void UpdateCartItem(CartItemDto cartItemDto);
        void DeleteCartItem(int cartItemId);
    }
}
