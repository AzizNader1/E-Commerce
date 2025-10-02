using E_Commerce.API.DTOs.UserDTOs;
using E_Commerce.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost]
        public IActionResult Login (LoginUserDto loginUserDto)
        {
            try
            {
                return Ok(_accountService.Login(loginUserDto));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
