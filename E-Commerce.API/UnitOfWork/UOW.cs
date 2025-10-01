using E_Commerce.API.Data;
using E_Commerce.API.Models;
using E_Commerce.API.Repositories;

namespace E_Commerce.API.UnitOfWork
{
    public class UOW
    {
        private readonly ApplicationDBContext _context;
        private GenericRepository<User> _userRepository;
        private GenericRepository<Product> _productRepository;
        private GenericRepository<Category> _categoryRepository;
        private GenericRepository<Cart> _cartRepository;
        private GenericRepository<CartItem> _cartItemRepository;
        private GenericRepository<Order> _orderRepository;
        private GenericRepository<OrderItem> _orderItemRepository;
        public UOW(ApplicationDBContext context)
        {
            _context = context;
        }
        public GenericRepository<User> UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new GenericRepository<User>(_context);
                }
                return _userRepository;
            }
        }
        public GenericRepository<Product> ProductRepository
        {
            get
            {
                if (_productRepository == null)
                {
                    _productRepository = new GenericRepository<Product>(_context);
                }
                return _productRepository;
            }
        }
        public GenericRepository<Category> CategoryRepository
        {
            get
            {
                if (_categoryRepository == null)
                {
                    _categoryRepository = new GenericRepository<Category>(_context);
                }
                return _categoryRepository;
            }
        }
        public GenericRepository<Cart> CartRepository
        {
            get
            {
                if (_cartRepository == null)
                {
                    _cartRepository = new GenericRepository<Cart>(_context);
                }
                return _cartRepository;
            }
        }
        public GenericRepository<CartItem> CartItemRepository
        {
            get
            {
                if (_cartItemRepository == null)
                {
                    _cartItemRepository = new GenericRepository<CartItem>(_context);
                }
                return _cartItemRepository;
            }
        }
        public GenericRepository<Order> OrderRepository
        {
            get
            {
                if (_orderRepository == null)
                {
                    _orderRepository = new GenericRepository<Order>(_context);
                }
                return _orderRepository;
            }
        }
        public GenericRepository<OrderItem> OrderItemRepository
        {
            get
            {
                if (_orderItemRepository == null)
                {
                    _orderItemRepository = new GenericRepository<OrderItem>(_context);
                }
                return _orderItemRepository;
            }
        }
    }
}
