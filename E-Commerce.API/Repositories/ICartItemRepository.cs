using E_Commerce.API.Models;

namespace E_Commerce.API.Repositories
{
    public interface ICartItemRepository
    {
        Task<CartItem> GetCartItemByIdAsync(int cartItemId);
        Task<IEnumerable<CartItem>> GetCartItemsByCartIdAsync(int cartId);
        Task AddCartItemAsync(CartItem cartItem);
        Task UpdateCartItemAsync(CartItem cartItem);
        Task DeleteCartItemAsync(int cartItemId);
    }
}
