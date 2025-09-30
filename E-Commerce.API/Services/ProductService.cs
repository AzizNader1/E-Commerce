using E_Commerce.API.DTOs;
using E_Commerce.API.Repositories;

namespace E_Commerce.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository _genericRepository;
        public ProductService(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public void AddAsync(ProductDto productDto)
        {
            throw new NotImplementedException();
        }

        public void DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public List<ProductDto> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public ProductDto GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateAsync(ProductDto productDto)
        {
            throw new NotImplementedException();
        }
    }
}
