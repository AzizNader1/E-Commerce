using E_Commerce.MVC.DTOs.UserDTOs;

namespace E_Commerce.MVC.Services
{
    public interface IApiUsersService
    {
        Task<List<UserDto>> GetAllUsersAsync();
        Task<UserDto?> GetUserByIdAsync(int id);
        Task<UserDto> GetUserByNameAsync(string userName);
        Task<LoginResponseDto> UpdateUserAsync(UserDto userDto);
        Task<bool> DeleteUserAsync(int id);
    }
}
