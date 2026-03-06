using E_Commerce.API.DTOs.UserDTOs;

namespace E_Commerce.API.Services
{
    public interface IUserService
    {
        List<UserDto> GetAllUsers();
        UserDto GetUserById(int userId);
        void AddUser(RegisterUserDto createUserDto);
        void UpdateUser(UserDto userDto);
        void DeleteUser(int userId);
    }
}
