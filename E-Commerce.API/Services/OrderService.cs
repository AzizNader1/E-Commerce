using E_Commerce.API.DTOs;
using E_Commerce.API.Repositories;

namespace E_Commerce.API.Services
{
    public class OrderService : IOrderService
    {
        private readonly IGenericRepository _genericRepository;
        public OrderService(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public void AddAsync(OrderDto orderDto)
        {
            throw new NotImplementedException();
        }

        public void DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public List<OrderDto> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public OrderDto GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateAsync(OrderDto orderDto)
        {
            throw new NotImplementedException();
        }
    }
}
