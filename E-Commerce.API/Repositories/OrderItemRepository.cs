using E_Commerce.API.Models;

namespace E_Commerce.API.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        public Task AddOrderItemAsync(OrderItem orderItem)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOrderItemAsync(int orderItemId)
        {
            throw new NotImplementedException();
        }

        public Task<OrderItem> GetOrderItemByIdAsync(int orderItemId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateOrderItemAsync(OrderItem orderItem)
        {
            throw new NotImplementedException();
        }
    }
}
