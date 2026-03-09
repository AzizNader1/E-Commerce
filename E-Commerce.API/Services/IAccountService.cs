using E_Commerce.API.DTOs.UserDTOs;

namespace E_Commerce.API.Services
{
    /// <summary>
    /// This interface defines the contract for the account service in an e-commerce application. It includes methods for user login, logout, registration, and password change. The Login method takes a LoginUserDto object and returns a LoginResponseDto object containing authentication details. The Logout method takes a user ID and returns a string message indicating the result of the logout operation. The Register method takes a RegisterUserDto object and returns a LoginResponseDto object with the registration result. The ChangePassword method takes a ChangePasswordDto object and returns a string message indicating the success or failure of the password change operation. This interface is essential for managing user accounts and authentication in the application.
    /// </summary>
    public interface IAccountService
    {
        LoginResponseDto Login(LoginUserDto loginUserDto);
        string Logout(int userId);
        LoginResponseDto Register(RegisterUserDto createUserDto);
        string ChangePassword(ChangePasswordDto changePasswordDto);

    }
}
