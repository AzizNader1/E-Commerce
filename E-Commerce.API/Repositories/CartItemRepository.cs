using E_Commerce.API.Models;

namespace E_Commerce.API.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        public Task AddCartItemAsync(CartItem cartItem)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCartItemAsync(int cartItemId)
        {
            throw new NotImplementedException();
        }

        public Task<CartItem> GetCartItemByIdAsync(int cartItemId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CartItem>> GetCartItemsByCartIdAsync(int cartId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCartItemAsync(CartItem cartItem)
        {
            throw new NotImplementedException();
        }
    }
}
