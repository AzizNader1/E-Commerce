using E_Commerce.API.DTOs;
using E_Commerce.API.Repositories;

namespace E_Commerce.API.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IGenericRepository _genericRepository;
        public OrderItemService(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public void AddAsync(OrderItemDto orderItemDto)
        {
            throw new NotImplementedException();
        }

        public void DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public List<OrderItemDto> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public OrderItemDto GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateAsync(OrderItemDto orderItemDto)
        {
            throw new NotImplementedException();
        }
    }
}
