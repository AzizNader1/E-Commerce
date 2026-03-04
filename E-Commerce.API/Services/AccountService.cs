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
            var users = _uow.UserRepository.GetAllAsync();
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

            _uow.UserRepository.AddAsync(newUser);

            loginResponseDto.UserName = newUser.UserName;
            loginResponseDto.UserRoles = [newUser.UserRole.ToString()];
            loginResponseDto.UserToken = GenerateToken(newUser);
            loginResponseDto.IsAuthenticated = true;

            return loginResponseDto;

        }

        public LoginResponseDto Login(LoginUserDto loginUserDto)
        {
            var loginResponseDto = new LoginResponseDto();

            var users = _uow.UserRepository.GetAllAsync();
            foreach (var user in users)
            {
                if (user.UserName == loginUserDto.UserName && user.UserPassword == loginUserDto.UserPassword && user.UserEmail == loginUserDto.UserEmail)
                {
                    loginResponseDto.IsAuthenticated = true;
                    loginResponseDto.UserName = user.UserName!;
                    loginResponseDto.UserRoles = [user.UserRole.ToString()];
                    loginResponseDto.UserToken = GenerateToken(user);
                    return loginResponseDto;
                }
            }

            loginResponseDto.IsAuthenticated = false;
            loginResponseDto.ErrorMessage = "Invalid username, email or password.";
            return loginResponseDto;
        }

        public void Logout(LoginUserDto loginUserDto)
        {

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
    }
}
