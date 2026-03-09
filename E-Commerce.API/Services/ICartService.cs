using E_Commerce.API.DTOs.CartDTOs;

namespace E_Commerce.API.Services
{
    /// <summary>
    /// This interface defines the contract for the cart service in an e-commerce application. It includes methods for retrieving all carts, retrieving a cart by its ID, retrieving carts by user ID or user name, adding a new cart, updating an existing cart, and deleting a cart. Each method is designed to handle specific operations related to shopping carts, ensuring that the application can manage users' carts effectively. The service is responsible for implementing the business logic associated with carts and interacting with the data layer to perform CRUD operations on cart data.
    /// </summary>
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
