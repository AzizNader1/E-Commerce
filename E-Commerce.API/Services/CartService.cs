using E_Commerce.API.DTOs.CartDTOs;
using E_Commerce.API.DTOs.CartItemDTOs;
using E_Commerce.API.Models;
using E_Commerce.API.UnitOfWork;

namespace E_Commerce.API.Services
{
    /// <summary>
    /// This class represents the service layer for managing shopping carts in an e-commerce application. It implements the ICartService interface and provides methods for adding, deleting, retrieving, and updating carts. The service interacts with the database through a Unit of Work (UOW) pattern to perform operations on cart data. Each method includes validation checks to ensure that the input data is valid and that the necessary related entities (such as users and cart items) exist in the database. The service is responsible for handling all business logic related to shopping carts, ensuring that the application functions correctly when users create or modify their carts.
    /// </summary>
    public class CartService : ICartService
    {
        private readonly UOW _uow;
        public CartService(UOW uow)
        {
            _uow = uow;
        }

        public void AddCart(CreateCartDto createCartDto)
        {
            if (createCartDto == null)
                throw new ArgumentNullException(nameof(createCartDto), "Cart data cannot be null");

            if (createCartDto.UserId <= 0)
                throw new ArgumentException("Invalid user ID", nameof(createCartDto.UserId));

            if (_uow.CartRepository.GetAllModels().Any(c => c.UserId == createCartDto.UserId))
                throw new InvalidOperationException("A cart already exists for the given user ID");

            _uow.CartRepository.AddModel(new Cart
            {
                UserId = createCartDto.UserId,
            });
        }

        public void DeleteCart(int cartId)
        {
            if (cartId <= 0)
                throw new ArgumentException("Invalid cart ID", nameof(cartId));

            if (_uow.CartRepository.GetModelById(cartId) == null)
                throw new ArgumentNullException("No cart found for the given ID");

            var cartItems = _uow.CartItemRepository.GetAllModels().Where(ci => ci.CartId == cartId).ToList();
            foreach (var cartItem in cartItems)
            {
                _uow.CartItemRepository.DeleteModel(cartItem.CartItemId);
            }

            _uow.CartRepository.DeleteModel(cartId);
        }

        public List<CartDto> GetAllCarts()
        {
            var carts = _uow.CartRepository.GetAllModels();
            if (carts == null || carts.Count == 0)
                throw new ArgumentNullException(nameof(carts), "No carts found in the database");

            var cartItemsDto = new List<CartItemDto>();
            var cartItems = _uow.CartItemRepository.GetAllModels();
            foreach (var cartItem in cartItems)
            {
                cartItemsDto.Add(new CartItemDto
                {
                    CartItemId = cartItem.CartItemId,
                    CartId = cartItem.CartId,
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity
                });
            }

            var cartDtos = new List<CartDto>();

            foreach (var cart in carts)
            {
                cartDtos.Add(new CartDto
                {
                    CartId = cart.CartId,
                    UserId = cart.UserId,
                    CartItems = cartItemsDto.Where(ci => ci.CartId == cart.CartId).ToList()
                });
            }
            return cartDtos;
        }

        public List<CartDto> GetAllCartsByUserId(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("Invalid user ID", nameof(userId));

            var user = _uow.UserRepository.GetModelById(userId);
            if (user == null)
                throw new ArgumentNullException(nameof(user), "No user found for the given ID");

            var carts = _uow.CartRepository.GetAllModels();
            if (carts == null || carts.Count == 0)
                throw new ArgumentNullException(nameof(carts), "No carts found in the database");

            var userCarts = carts.Where(c => c.UserId == userId).ToList();
            if (userCarts == null || userCarts.Count == 0)
                throw new ArgumentNullException(nameof(userCarts), "No carts found for the given user ID");

            var cartItemsDto = new List<CartItemDto>();
            var cartItems = _uow.CartItemRepository.GetAllModels();
            foreach (var cartItem in cartItems)
            {
                cartItemsDto.Add(new CartItemDto
                {
                    CartItemId = cartItem.CartItemId,
                    CartId = cartItem.CartId,
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity
                });
            }

            var cartDtos = new List<CartDto>();
            foreach (var cart in userCarts)
            {
                cartDtos.Add(new CartDto
                {
                    CartId = cart.CartId,
                    UserId = cart.UserId,
                    CartItems = cartItemsDto.Where(ci => ci.CartId == cart.CartId).ToList()
                });
            }
            return cartDtos;
        }

        public List<CartDto> GetAllCartsByUserName(string userName)
        {
            if (userName == null || userName == default || userName == "")
                throw new ArgumentException("Invalid user name", nameof(userName));

            var user = _uow.UserRepository.GetAllModels().FirstOrDefault(u => u.UserName == userName);
            if (user == null)
                throw new ArgumentNullException(nameof(user), "No user found for the given name");

            var carts = _uow.CartRepository.GetAllModels();
            if (carts == null || carts.Count == 0)
                throw new ArgumentNullException(nameof(carts), "No carts found in the database");

            var userCarts = carts.Where(c => c.UserId == user.UserId).ToList();
            if (userCarts == null || userCarts.Count == 0)
                throw new ArgumentNullException(nameof(userCarts), "No carts found for the given user name");

            var cartItemsDto = new List<CartItemDto>();
            var cartItems = _uow.CartItemRepository.GetAllModels();
            foreach (var cartItem in cartItems)
            {
                cartItemsDto.Add(new CartItemDto
                {
                    CartItemId = cartItem.CartItemId,
                    CartId = cartItem.CartId,
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity
                });
            }

            var cartDtos = new List<CartDto>();
            foreach (var cart in userCarts)
            {
                cartDtos.Add(new CartDto
                {
                    CartId = cart.CartId,
                    UserId = cart.UserId,
                    CartItems = cartItemsDto.Where(ci => ci.CartId == cart.CartId).ToList()
                });
            }
            return cartDtos;
        }

        public CartDto GetCartById(int cartId)
        {
            if (cartId <= 0)
                throw new ArgumentException("Invalid cart ID", nameof(cartId));

            var cart = _uow.CartRepository.GetModelById(cartId);
            if (cart == null)
                throw new ArgumentNullException(nameof(cart), "No cart found for the given ID");

            var cartItemsDto = new List<CartItemDto>();
            var cartItems = _uow.CartItemRepository.GetAllModels().Where(ci => ci.CartId == cartId).ToList();
            foreach (var cartItem in cartItemsDto)
            {
                cartItemsDto.Add(new CartItemDto
                {
                    CartItemId = cartItem.CartItemId,
                    CartId = cartItem.CartId,
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity
                });
            }

            return new CartDto
            {
                CartId = cart.CartId,
                UserId = cart.UserId,
                CartItems = cartItemsDto
            };
        }

        public void UpdateCart(CartDto cartDto)
        {
            if (cartDto == null)
                throw new ArgumentNullException(nameof(cartDto), "Cart data cannot be null");

            Cart existingCart = _uow.CartRepository.GetModelById(cartDto.CartId);
            if (existingCart == null)
                throw new ArgumentNullException(nameof(existingCart), "No cart found for the given ID");

            existingCart.UserId = cartDto.UserId;
            _uow.CartRepository.UpdateModel(existingCart);
        }
    }
}
