using E_Commerce.API.DTOs;
using E_Commerce.API.Repositories;

namespace E_Commerce.API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository _genericRepository;
        public CategoryService(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public void AddAsync(CategoryDto categoryDto)
        {
            throw new NotImplementedException();
        }

        public void DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public List<CategoryDto> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public CategoryDto GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateAsync(CategoryDto categoryDto)
        {
            throw new NotImplementedException();
        }
    }
}
