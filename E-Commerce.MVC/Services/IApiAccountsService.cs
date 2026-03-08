using E_Commerce.MVC.DTOs.UserDTOs;

namespace E_Commerce.MVC.Services
{
    public interface IApiAccountsService
    {
        Task<LoginResponseDto> LoginAsync(LoginUserDto loginUserDto);
        Task<LoginResponseDto> RegisterAsync(RegisterUserDto registerUserDto);
    }
}
