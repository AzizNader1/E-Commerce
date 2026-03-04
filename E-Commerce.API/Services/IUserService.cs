using E_Commerce.API.DTOs.UserDTOs;

namespace E_Commerce.API.Services
{
    public interface IUserService
    {
        List<UserDto> GetAllUsersAsync();
        UserDto GetUserByIdAsync(int userId);
        void AddUserAsync(RegisterUserDto createUserDto);
        void UpdateUserAsync(UserDto userDto);
        void DeleteUserAsync(int userId);
    }
}
