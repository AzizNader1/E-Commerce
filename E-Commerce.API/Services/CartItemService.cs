using E_Commerce.API.DTOs.CartItemDTOs;
using E_Commerce.API.Models;
using E_Commerce.API.Repositories;
using E_Commerce.API.UnitOfWork;

namespace E_Commerce.API.Services
{
    public class CartItemService : ICartItemService
    {
        private readonly UOW _uow;
        public CartItemService(UOW uow)
        {
            _uow = uow;
        }

        public void AddCartItemAsync(CreateCartItemDto createCartItemDto)
        {
            if (createCartItemDto == null)
                throw new ArgumentNullException(nameof(createCartItemDto), "Cart item data can not be null");

            _uow.CartItemRepository.AddAsync(new CartItem { 
                CartId = createCartItemDto.CartId,
                ProductId = createCartItemDto.ProductId,
                Quantity = createCartItemDto.Quantity,
            });
        }

        public void DeleteCartItemAsync(int cartItemId)
        {
            if (cartItemId <= 0)
                throw new ArgumentException("Invalid cart item ID", nameof(cartItemId));

            CartItem selectedCartItem = _uow.CartItemRepository.GetByIdAsync(cartItemId);
            if (selectedCartItem == null)
                throw new ArgumentNullException(nameof(selectedCartItem), "No cart item found for the given ID");

            _uow.CartItemRepository.DeleteAsync(cartItemId);
        }

        public List<CartItemDto> GetAllCartItemsAsync()
        {
            List<CartItem> cartItems = _uow.CartItemRepository.GetAllAsync();
            if (cartItems == null || cartItems.Count == 0)
                throw new ArgumentNullException(nameof(cartItems), "No cart items found in the database");

            List<CartItemDto> cartItemDtos = new List<CartItemDto>();
            
            foreach (var cartItem in cartItems)
            {
                cartItemDtos.Add(new CartItemDto
                {
                    CartItemId = cartItem.CartItemId,
                    CartId = cartItem.CartId,
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity
                });
            }
            return cartItemDtos;
        }

        public CartItemDto GetCartItemByIdAsync(int cartItemId)
        {
            if (cartItemId <= 0)
                throw new ArgumentException("Invalid cart item ID", nameof(cartItemId));

            CartItem selectedCartItem = _uow.CartItemRepository.GetByIdAsync(cartItemId);
            if (selectedCartItem == null)
                throw new ArgumentNullException(nameof(selectedCartItem), "No cart item found for the given ID");

            return new CartItemDto
            {
                CartItemId = selectedCartItem.CartItemId,
                CartId = selectedCartItem.CartId,
                ProductId = selectedCartItem.ProductId,
                Quantity = selectedCartItem.Quantity
            };
        }

        public List<CartItemDto> GetCartItemsByCartIdAsync(int cartId)
        {
            if (cartId <= 0)
                throw new ArgumentException("Invalid cart ID", nameof(cartId));

            List<CartItem> cartItems = _uow.CartItemRepository.GetAllAsync();
            if (cartItems == null || cartItems.Count == 0)
                throw new ArgumentNullException(nameof(cartItems), "No cart items found in the database");

            List<CartItemDto> cartItemDtos = new List<CartItemDto>();
            foreach (var cartItem in cartItems)
            {
                if (cartItem.CartId == cartId)
                {
                    cartItemDtos.Add(new CartItemDto
                    {
                        CartItemId = cartItem.CartItemId,
                        CartId = cartItem.CartId,
                        ProductId = cartItem.ProductId,
                        Quantity = cartItem.Quantity
                    });
                }
            }
            return cartItemDtos;
        }

        public void UpdateCartItemAsync(CartItemDto cartItemDto)
        {
            if (cartItemDto == null)
                throw new ArgumentNullException(nameof(cartItemDto), "Cart item data can not be null");

            CartItem existingCartItem = _uow.CartItemRepository.GetByIdAsync(cartItemDto.CartItemId);
            if (existingCartItem == null)
                throw new ArgumentNullException(nameof(existingCartItem), "No cart item found for the given ID");

            _uow.CartItemRepository.UpdateAsync(new CartItem
            {
                Quantity = cartItemDto.Quantity
            });
        }
    }
}
