using E_Commerce.API.DTOs;
using E_Commerce.API.Repositories;

namespace E_Commerce.API.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository _genericRepository;
        public UserService(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public void AddAsync(UserDto userDto)
        {
            throw new NotImplementedException();
        }

        public void DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public List<UserDto> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public UserDto GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateAsync(UserDto userDto)
        {
            throw new NotImplementedException();
        }
    }
}
