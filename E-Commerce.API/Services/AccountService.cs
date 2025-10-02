using E_Commerce.API.DTOs.UserDTOs;
using E_Commerce.API.Models;
using E_Commerce.API.UnitOfWork;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;

namespace E_Commerce.API.Services
{
    public class AccountService : IAccountService
    {
        private readonly UOW _uow;
        public AccountService(UOW uow)
        {
            _uow = uow;
        }

        public string Login(LoginUserDto loginUserDto)
        {
            if (loginUserDto == null)
                throw new ArgumentNullException(nameof(loginUserDto),"the data can not be null");

            List<User> users = _uow.UserRepository.GetAllAsync();
            if (users.Count == 0)
                throw new ArgumentNullException(nameof(users), "there is no users in the database");

            var wantedUser = users.Select(u => u.Username == loginUserDto.UserName && u.Email == loginUserDto.Email && u.Password == loginUserDto.Password);
            if (wantedUser == null)
                throw new ArgumentNullException(nameof(wantedUser), "there is no data exists for these credentails");

            List<Claim> userClaims = new List<Claim>();
            userClaims.Add(new Claim("UserName",loginUserDto.UserName.ToString()));
            userClaims.Add(new Claim("UserEmail",loginUserDto.Email.ToString()));
            userClaims.Add(new Claim("UserPassword",loginUserDto.Password.ToString()));

            string secretKey = "welcome to my programming world while you can convert your dreams into reality";
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            var signingCredentails = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: userClaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: signingCredentails
                );

            var stringToken =  new JwtSecurityTokenHandler().WriteToken(token);
            return stringToken;

            //another way to minimize the size of the code

            //return new JwtSecurityTokenHandler()
            //    .WriteToken(new JwtSecurityToken(
            //     claims: userClaims,
            //     expires: DateTime.Now.AddDays(1),
            //     signingCredentials: new SigningCredentials(
            //        new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
            //        SecurityAlgorithms.HmacSha256)
            //     ));
        }

        public void Logout(LoginUserDto loginUserDto)
        {
            
        }

        public void Register(CreateUserDto createUserDto)
        {
            
        }
    }
}
