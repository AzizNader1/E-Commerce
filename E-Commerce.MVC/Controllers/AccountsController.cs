using E_Commerce.MVC.DTOs.UserDTOs;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.MVC.Controllers
{
    public class AccountsController : Controller
    {
        public AccountsController()
        {
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginUserDto loginUserDto)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterUserDto registerUserDto)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Logout(LoginUserDto loginUserDto)
        {
            return View();
        }
    }
}
