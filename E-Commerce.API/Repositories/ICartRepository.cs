using E_Commerce.API.Models;

namespace E_Commerce.API.Repositories
{
    public interface ICartRepository
    {
        Task<Cart> GetCartByUserIdAsync(int userId);
        Task<Cart> GetCartByIdAsync(int id);
        Task AddCartAsync(Cart cart);
        Task UpdateCartAsync(Cart cart);
        Task DeleteCartAsync(int cartId);
    }
}
