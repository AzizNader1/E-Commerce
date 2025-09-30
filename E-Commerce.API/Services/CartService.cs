using E_Commerce.API.DTOs;
using E_Commerce.API.Repositories;

namespace E_Commerce.API.Services
{
    public class CartService : ICartService
    {
        private readonly IGenericRepository _genericRepository;
        public CartService(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public void AddAsync(CartDto cartDto)
        {
            throw new NotImplementedException();
        }

        public void DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public List<CartDto> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public CartDto GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateAsync(CartDto cartDto)
        {
            throw new NotImplementedException();
        }
    }
}
