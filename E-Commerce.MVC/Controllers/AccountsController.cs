using E_Commerce.MVC.DTOs.UserDTOs;
using E_Commerce.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.MVC.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IApiAccountsService _apiAccountsService;
        private readonly IApiUsersService _apiUsersService;

        public AccountsController(IApiAccountsService apiAccountsService, IApiUsersService apiUsersService)
        {
            _apiAccountsService = apiAccountsService;
            _apiUsersService = apiUsersService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserDto loginUserDto)
        {
            if (!ModelState.IsValid)
                return View(loginUserDto);

            var response = await _apiAccountsService.LoginAsync(loginUserDto);

            if (response.IsAuthenticated)
            {
                HttpContext.Session.SetString("UserName", response.UserName ?? "");
                TempData["UserName"] = response.UserName;
                HttpContext.Session.SetString("UserRole", response.UserRoles.ToString() ?? "");
                TempData["UserRole"] = response.UserRoles.ToString();
                HttpContext.Session.SetString("UserToken", response.UserToken ?? "");
                TempData["UserToken"] = response.UserToken;
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ErrorMessage = response.ErrorMessage == string.Empty ? "Invalid login attempt." : response.ErrorMessage;
            return View(loginUserDto);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
        {
            if (!ModelState.IsValid)
                return View(registerUserDto);

            var response = await _apiAccountsService.RegisterAsync(registerUserDto);

            if (response.IsAuthenticated)
            {
                AddUserCreadentials(response);
                return RedirectToAction("Index", "Home");
            }
            ViewBag.ErrorMessage = response.ErrorMessage == string.Empty ? "Registration failed." : response.ErrorMessage;
            return View(registerUserDto);
        }

        [HttpGet]
        public async Task<IActionResult> UserProfile(string userName)
        {
            var userDto = new UserDto();
            userDto = await _apiUsersService.GetUserByNameAsync(userName);

            TempData["UserName"] = HttpContext.Session.GetString("UserName");
            TempData["UserRole"] = HttpContext.Session.GetString("UserRole");
            TempData["UserToken"] = HttpContext.Session.GetString("UserToken");
            return View(userDto);
        }

        [HttpPost]
        public async Task<IActionResult> UserProfile(UserDto userDto)
        {
            if (!ModelState.IsValid)
                return View(userDto);

            var response = await _apiUsersService.UpdateUserAsync(userDto);

            if (response.IsAuthenticated)
            {
                AddUserCreadentials(response);
                ViewBag.SuccessMessage = string.IsNullOrWhiteSpace(response.ErrorMessage)
                    ? "Your profile has been updated successfully."
                    : response.ErrorMessage;
                return View(userDto);
            }

            ViewBag.ErrorMessage = response.ErrorMessage == string.Empty ? "Profile update failed." : response.ErrorMessage;
            return View(userDto);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            RemoveUserCreadentials();
            return RedirectToAction("Index", "Home");
        }

        private void AddUserCreadentials(LoginResponseDto loginResponseDto)
        {
            HttpContext.Session.SetString("UserName", loginResponseDto.UserName ?? "");
            TempData["UserName"] = loginResponseDto.UserName;
            HttpContext.Session.SetString("UserRole", loginResponseDto.UserRoles.ToString() ?? "");
            TempData["UserRole"] = loginResponseDto.UserRoles.ToString();
            HttpContext.Session.SetString("UserToken", loginResponseDto.UserToken ?? "");
            TempData["UserToken"] = loginResponseDto.UserToken;
        }
        private void RemoveUserCreadentials()
        {
            HttpContext.Session.SetString("UserName", "");
            TempData["UserName"] = "";
            HttpContext.Session.SetString("UserRole", "");
            TempData["UserRole"] = "";
            HttpContext.Session.SetString("UserToken", "");
            TempData["UserToken"] = "";
        }
    }
}
