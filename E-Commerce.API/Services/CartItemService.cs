using E_Commerce.API.DTOs.CartItemDTOs;
using E_Commerce.API.Models;
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

        public void AddCartItem(CreateCartItemDto createCartItemDto)
        {
            if (createCartItemDto == null)
                throw new ArgumentNullException(nameof(createCartItemDto), "Cart item data can not be null");

            if (!_uow.CartRepository.GetAllModels().Any(c => c.CartId == createCartItemDto.CartId))
                throw new ArgumentException("No cart found for the given cart ID", nameof(createCartItemDto.CartId));

            var product = _uow.ProductRepository.GetModelById(createCartItemDto.ProductId);
            if (product == null)
                throw new ArgumentException("No product found for the given product ID", nameof(createCartItemDto.ProductId));

            if (createCartItemDto.Quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero", nameof(createCartItemDto.Quantity));

            if (createCartItemDto.Quantity > product.ProductStockQuantity)
                throw new ArgumentException("Requested quantity exceeds available stock", nameof(createCartItemDto.Quantity));

            _uow.CartItemRepository.AddModel(new CartItem
            {
                CartId = createCartItemDto.CartId,
                ProductId = createCartItemDto.ProductId,
                Quantity = createCartItemDto.Quantity,
            });
        }

        public void DeleteCartItem(int cartItemId)
        {
            if (cartItemId <= 0)
                throw new ArgumentException("Invalid cart item ID", nameof(cartItemId));

            var selectedCartItem = _uow.CartItemRepository.GetModelById(cartItemId);
            if (selectedCartItem == null)
                throw new ArgumentNullException(nameof(selectedCartItem), "No cart item found for the given ID");

            _uow.CartItemRepository.DeleteModel(cartItemId);
        }

        public List<CartItemDto> GetAllCartItems()
        {
            var cartItems = _uow.CartItemRepository.GetAllModels();
            if (cartItems == null || cartItems.Count == 0)
                throw new ArgumentNullException(nameof(cartItems), "No cart items found in the database");

            var cartItemDtos = new List<CartItemDto>();

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

        public CartItemDto GetCartItemById(int cartItemId)
        {
            if (cartItemId <= 0)
                throw new ArgumentException("Invalid cart item ID", nameof(cartItemId));

            var selectedCartItem = _uow.CartItemRepository.GetModelById(cartItemId);
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

        public List<CartItemDto> GetCartItemsByCartId(int cartId)
        {
            if (cartId <= 0)
                throw new ArgumentException("Invalid cart ID", nameof(cartId));

            var cartItems = _uow.CartItemRepository.GetAllModels();
            if (cartItems == null || cartItems.Count == 0)
                throw new ArgumentNullException(nameof(cartItems), "No cart items found in the database");

            if (!cartItems.Any(c => c.CartId == cartId))
                throw new ArgumentException("No cart items found for the given cart ID", nameof(cartId));

            var cartItemDtos = new List<CartItemDto>();
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

        public void UpdateCartItem(CartItemDto cartItemDto)
        {
            if (cartItemDto == null)
                throw new ArgumentNullException(nameof(cartItemDto), "Cart item data can not be null");

            var existingCartItem = _uow.CartItemRepository.GetModelById(cartItemDto.CartItemId);
            if (existingCartItem == null)
                throw new ArgumentNullException(nameof(existingCartItem), "No cart item found for the given ID");

            _uow.CartItemRepository.UpdateModel(new CartItem
            {
                Quantity = cartItemDto.Quantity
            });
        }
    }
}
