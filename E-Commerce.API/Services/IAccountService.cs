using E_Commerce.API.DTOs.UserDTOs;
using Microsoft.AspNetCore.Identity.Data;

namespace E_Commerce.API.Services
{
    public interface IAccountService
    {
        string Login(LoginUserDto loginUserDto);
        void Logout(LoginUserDto loginUserDto);
        void Register(CreateUserDto createUserDto);

    }
}
