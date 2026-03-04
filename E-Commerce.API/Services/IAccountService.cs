using E_Commerce.API.DTOs.UserDTOs;

namespace E_Commerce.API.Services
{
    public interface IAccountService
    {
        LoginResponseDto Login(LoginUserDto loginUserDto);
        void Logout(LoginUserDto loginUserDto);
        LoginResponseDto Register(RegisterUserDto createUserDto);

    }
}
