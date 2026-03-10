using E_Commerce.MVC.DTOs.UserDTOs;

namespace E_Commerce.MVC.Services
{
    public interface IApiUsersService
    {
        Task<UserDto> GetUserByNameAsync(string userName);
        Task<LoginResponseDto> UpdateUserAsync(UserDto userDto);
    }
}
