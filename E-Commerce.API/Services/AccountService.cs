using E_Commerce.API.DTOs.UserDTOs;
using E_Commerce.API.Helpers;
using E_Commerce.API.Models;
using E_Commerce.API.UnitOfWork;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_Commerce.API.Services
{
    /// <summary>
    /// This class represents the account service for an e-commerce application. It implements the IAccountService interface and provides methods for user registration, login, logout, token generation, and password change. The service interacts with the database through a Unit of Work (UOW) pattern to manage user data. It also utilizes JWT (JSON Web Tokens) for authentication and authorization purposes. The Register method checks for existing users and creates a new user if the email and username are unique. The Login method validates user credentials and generates a JWT token upon successful authentication. The Logout method deletes the user from the database, and the ChangePassword method allows users to update their passwords securely. Overall, this service is responsible for handling all account-related operations in the application.
    /// </summary>
    public class AccountService : IAccountService
    {
        private readonly UOW _uow;
        private readonly JWT _jwtSettings;
        public AccountService(UOW uow, IOptions<JWT> jwtSettings)
        {
            _uow = uow;
            _jwtSettings = jwtSettings.Value;
        }

        public LoginResponseDto Register(RegisterUserDto createUserDto)
        {
            var loginResponseDto = new LoginResponseDto();
            var users = _uow.UserRepository.GetAllModels();
            foreach (var user in users)
            {
                if (user.UserEmail == createUserDto.UserEmail)
                {
                    loginResponseDto.ErrorMessage = "this email is already exists";
                    return loginResponseDto;
                }
                if (user.UserName == createUserDto.UserName)
                {
                    loginResponseDto.ErrorMessage = "this username is already exists";
                    return loginResponseDto;
                }
            }

            var newUser = new User
            {
                UserName = createUserDto.UserName,
                UserEmail = createUserDto.UserEmail,
                UserPassword = createUserDto.UserPassword,
                UserFullName = createUserDto.UserFullName,
                UserAddress = createUserDto.UserAddress,
                UserPhoneNumber = createUserDto.UserPhoneNumber,
                UserRole = createUserDto.UserName.Contains("admin", StringComparison.OrdinalIgnoreCase) ? UserRoles.Admin : UserRoles.Customer
            };

            _uow.UserRepository.AddModel(newUser);

            loginResponseDto.UserName = newUser.UserName;
            loginResponseDto.UserRoles = newUser.UserRole.ToString();
            loginResponseDto.UserToken = GenerateToken(newUser);
            loginResponseDto.IsAuthenticated = true;
            loginResponseDto.UserId = _uow.UserRepository.GetAllModels().FirstOrDefault(u => u.UserEmail == newUser.UserEmail)?.UserId ?? 0;

            _uow.CartRepository.AddModel(new Cart
            {
                UserId = newUser.UserId
            });

            return loginResponseDto;

        }

        public LoginResponseDto Login(LoginUserDto loginUserDto)
        {
            var loginResponseDto = new LoginResponseDto();

            var users = _uow.UserRepository.GetAllModels();
            foreach (var user in users)
            {
                if (user.UserName == loginUserDto.UserName && user.UserPassword == loginUserDto.UserPassword && user.UserEmail == loginUserDto.UserEmail)
                {
                    loginResponseDto.IsAuthenticated = true;
                    loginResponseDto.UserName = user.UserName!;
                    loginResponseDto.UserRoles = user.UserRole.ToString();
                    loginResponseDto.UserToken = GenerateToken(user);
                    loginResponseDto.UserId = user.UserId;
                    return loginResponseDto;
                }
            }

            loginResponseDto.IsAuthenticated = false;
            loginResponseDto.ErrorMessage = "Invalid username, email or password. Try to register first";
            return loginResponseDto;
        }

        public string Logout(int userId)
        {
            var user = _uow.UserRepository.GetModelById(userId);
            if (user == null)
                return "User not found.";

            _uow.UserRepository.DeleteModel(userId);
            return "User logged out successfully.";
        }

        public string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new (ClaimTypes.Name, user.UserName!),
                new (ClaimTypes.Email, user.UserEmail!),
                new (ClaimTypes.Role, user.UserRole.ToString())
            };

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwtSettings.DurationInDays),
                signingCredentials: signingCredentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return token;

            // generated token in one line of code without using the claims, symmetricSecurityKey, signingCredentials and jwtSecurityToken variables
            //return new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
            //    issuer: _jwtSettings.Issuer,
            //    audience: _jwtSettings.Audience,
            //    claims:
            //    [
            //        new (ClaimTypes.Name, user.UserName!),
            //        new (ClaimTypes.Email, user.UserEmail!),
            //        new (ClaimTypes.Role, user.UserRole.ToString())
            //    ],
            //    expires: DateTime.Now.AddDays(_jwtSettings.DurationInDays),
            //    signingCredentials: new SigningCredentials(
            //        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey)),
            //        SecurityAlgorithms.HmacSha256)));


        }

        public string ChangePassword(ChangePasswordDto changePasswordDto)
        {
            if (changePasswordDto.UserId <= 0)
                return "Invalid user ID.";

            if (changePasswordDto.NewPassword == "stringstring" || changePasswordDto.ConfirmNewPassword == "stringstring")
                return "please change the data from default values to your own values";

            var user = _uow.UserRepository.GetModelById(changePasswordDto.UserId);
            if (user == null)
                return "User not found.";

            if (user.UserPassword != changePasswordDto.CurrentPassword)
                return "Current password is incorrect.";

            if (changePasswordDto.NewPassword != changePasswordDto.ConfirmNewPassword)
                return "New password and confirm new password do not match.";

            user.UserPassword = changePasswordDto.NewPassword;
            _uow.UserRepository.UpdateModel(user);
            return "Password changed successfully.";
        }
    }
}