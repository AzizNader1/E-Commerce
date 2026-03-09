using E_Commerce.API.DTOs.CartItemDTOs;

namespace E_Commerce.API.Services
{
    /// <summary>
    /// This interface defines the contract for the cart item service in an e-commerce application. It includes methods for retrieving all cart items, retrieving a cart item by its ID, retrieving cart items by cart ID, adding a new cart item, updating an existing cart item, and deleting a cart item. Each method is designed to handle specific operations related to cart items, ensuring that the application can manage the items in users' shopping carts effectively. The service is responsible for implementing the business logic associated with cart items and interacting with the data layer to perform CRUD operations on cart item data.
    /// </summary>
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
