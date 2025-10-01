using E_Commerce.API.DTOs.CartDTOs;
using E_Commerce.API.Models;
using E_Commerce.API.Repositories;
using E_Commerce.API.UnitOfWork;

namespace E_Commerce.API.Services
{
    public class CartService : ICartService
    {
        private readonly UOW _uow;
        public CartService(UOW uow)
        {
            _uow = uow;
        }

        public void AddCartAsync(CreateCartDto createCartDto)
        {
            if (createCartDto == null)
                throw new ArgumentNullException(nameof(createCartDto), "Cart data cannot be null");

            _uow.CartRepository.AddAsync(new Cart
            {
                UserId = createCartDto.UserId,
            });
        }

        public void DeleteCartAsync(int cartId)
        {
            if (cartId <= 0)
                throw new ArgumentException("Invalid cart ID", nameof(cartId));

            Cart selectedCart = _uow.CartRepository.GetByIdAsync(cartId);
            if (selectedCart == null)
                throw new ArgumentNullException(nameof(selectedCart), "No cart found for the given ID");

            _uow.CartRepository.DeleteAsync(cartId);
        }

        public List<CartDto> GetAllCartsAsync()
        {
            List<Cart> carts = _uow.CartRepository.GetAllAsync();
            if (carts == null || carts.Count == 0)
                throw new ArgumentNullException(nameof(carts), "No carts found in the database");

            List<CartDto> cartDtos = new List<CartDto>();

            foreach (var cart in carts)
            {
                cartDtos.Add(new CartDto
                {
                    CartId = cart.CartId,
                    UserId = cart.UserId
                });
            }
            return cartDtos;
        }

        public List<CartDto> GetAllCartsByUserIdAsync(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("Invalid user ID", nameof(userId));

            User user = _uow.UserRepository.GetByIdAsync(userId);
            if (user == null)
                throw new ArgumentNullException(nameof(user), "No user found for the given ID");

            List<Cart> carts = _uow.CartRepository.GetAllAsync();
            if (carts == null || carts.Count == 0)
                throw new ArgumentNullException(nameof(carts), "No carts found in the database");
           
            List<Cart> userCarts = carts.Where(c => c.UserId == userId).ToList();
            if (userCarts == null || userCarts.Count == 0)
                throw new ArgumentNullException(nameof(userCarts), "No carts found for the given user ID");

            List<CartDto> cartDtos = new List<CartDto>();
            foreach (var cart in userCarts)
            {
                cartDtos.Add(new CartDto
                {
                    CartId = cart.CartId,
                    UserId = cart.UserId
                });
            }
            return cartDtos;
        }

        public List<CartDto> GetAllCartsByUserNameAsync(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentException("Invalid user name", nameof(userName));

            List<User> users = _uow.UserRepository.GetAllAsync();
            if (users == null || users.Count == 0)
                throw new ArgumentNullException(nameof(users), "No users found in the database");

            User user = users.FirstOrDefault(u => u.Username.Equals(userName, StringComparison.OrdinalIgnoreCase))!;
            
            List<Cart> carts = _uow.CartRepository.GetAllAsync();
            if (carts == null || carts.Count == 0)
                throw new ArgumentNullException(nameof(carts), "No carts found in the database");

            List<Cart> userCarts = carts.Where(c => c.UserId == user.UserId).ToList();
            if (userCarts == null || userCarts.Count == 0)
                throw new ArgumentNullException(nameof(userCarts), "No carts found for the given user ID");

            List<CartDto> cartDtos = new List<CartDto>();
            foreach (var cart in userCarts)
            {
                cartDtos.Add(new CartDto
                {
                    CartId = cart.CartId,
                    UserId = cart.UserId
                });
            }
            return cartDtos;
        }

        public CartDto GetCartByIdAsync(int cartId)
        {
            if (cartId <= 0)
                throw new ArgumentException("Invalid cart ID", nameof(cartId));

            Cart cart = _uow.CartRepository.GetByIdAsync(cartId);
            if (cart == null)
                throw new ArgumentNullException(nameof(cart), "No cart found for the given ID");

            return new CartDto
            {
                CartId = cart.CartId,
                UserId = cart.UserId
            };
        }

        public void UpdateCartAsync(CartDto cartDto)
        {
            if (cartDto == null)
                throw new ArgumentNullException(nameof(cartDto), "Cart data cannot be null");

            Cart existingCart = _uow.CartRepository.GetByIdAsync(cartDto.CartId);
            if (existingCart == null)
                throw new ArgumentNullException(nameof(existingCart), "No cart found for the given ID");

            existingCart.UserId = cartDto.UserId;
            _uow.CartRepository.UpdateAsync(existingCart);
        }
    }
}
