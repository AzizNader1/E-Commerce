using E_Commerce.API.DTOs;
using E_Commerce.API.Repositories;

namespace E_Commerce.API.Services
{
    public class CartItemService : ICartItemService
    {
        private readonly IGenericRepository _genericRepository;
        public CartItemService(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public void AddAsync(CartItemDto cartItemDto)
        {
            throw new NotImplementedException();
        }

        public void DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public List<CartItemDto> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public CartItemDto GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateAsync(CartItemDto cartItemDto)
        {
            throw new NotImplementedException();
        }
    }
}
