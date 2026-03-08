using E_Commerce.MVC.DTOs.UserDTOs;
using E_Commerce.MVC.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce.MVC.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IApiAccountsService _apiAccountsService;

        public AccountsController(IApiAccountsService apiAccountsService)
        {
            _apiAccountsService = apiAccountsService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

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
                await SignInUser(response);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, response.ErrorMessage ?? "Invalid login attempt.");
            return View(loginUserDto);
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

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
                await SignInUser(response);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, response.ErrorMessage ?? "Registration failed.");
            return View(registerUserDto);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> LogoutPost()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        private async Task SignInUser(LoginResponseDto response)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, response.UserName ?? ""),
                new Claim("Token", response.UserToken ?? "")
            };

            if (response.UserRoles != null)
            {
                foreach (var role in response.UserRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            // Set cookie expiration
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7)
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);
        }
    }
}
