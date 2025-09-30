using E_Commerce.API.DTOs;

namespace E_Commerce.API.Services
{
    public interface IUserService
    {
        List<UserDto> GetAllAsync();
        UserDto GetByIdAsync(int id);
        void AddAsync(UserDto userDto);
        void UpdateAsync(UserDto userDto);
        void DeleteAsync(int id);
    }
}
