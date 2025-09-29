using E_Commerce.API.Models;

namespace E_Commerce.API.Repositories
{
    public class CartRepository : ICartRepository
    {
        public Task AddCartAsync(Cart cart)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCartAsync(int cartId)
        {
            throw new NotImplementedException();
        }

        public Task<Cart> GetCartByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Cart> GetCartByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCartAsync(Cart cart)
        {
            throw new NotImplementedException();
        }
    }
}
