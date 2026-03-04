using E_Commerce.API.DTOs.UserDTOs;
using E_Commerce.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register([FromBody] RegisterUserDto registerUserDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (registerUserDto.UserName == "string"
                    || registerUserDto.UserEmail == "user@example.com"
                    || registerUserDto.UserAddress == "string"
                    || registerUserDto.UserPassword == "stringstring"
                    || registerUserDto.UserFullName == "string")
                    return BadRequest("please change the data from default values to your own values");

                return Ok(_accountService.Register(registerUserDto));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] LoginUserDto loginUserDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (loginUserDto.UserName == "string"
                    || loginUserDto.UserEmail == "user@example.com"
                    || loginUserDto.UserPassword == "stringstring")
                    return BadRequest("please change the data from default values to your own values");

                return Ok(_accountService.Login(loginUserDto));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //[HttpPost]
        //public IActionResult Logout(LogoutUserDto logoutUserDto)
        //{
        //    try
        //    {
        //        return Ok(_accountService.Login(loginUserDto));
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}
    }
}
