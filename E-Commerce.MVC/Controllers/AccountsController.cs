using E_Commerce.MVC.DTOs.OtpDTOs;
using E_Commerce.MVC.DTOs.UserDTOs;
using E_Commerce.MVC.Helpers;
using E_Commerce.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.MVC.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IApiAccountsService _apiAccountsService;
        private readonly IApiUsersService _apiUsersService;
        private readonly IApiCartsService _apiCartsService;

        public AccountsController(IApiAccountsService apiAccountsService,
            IApiUsersService apiUsersService,
            IApiCartsService apiCartsService)
        {
            _apiAccountsService = apiAccountsService;
            _apiUsersService = apiUsersService;
            _apiCartsService = apiCartsService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserDto loginUserDto)
        {

            if (loginUserDto.UserName.Contains("admin") && loginUserDto.UserEmail.Contains("admin@gmail.com"))
            {
                HttpContext.Session.SetString("UserName", loginUserDto.UserName);
                HttpContext.Session.SetString("UserRole", "Admin");
                HttpContext.Session.SetString("UserEmail", loginUserDto.UserEmail);
                return RedirectToAction("Index", "Admin");
            }

            var response = await _apiAccountsService.LoginAsync(loginUserDto);

            if (response.IsAuthenticated)
            {
                //AddUserCreadentials(response);
                //return RedirectToAction("Index", "Home");


                // Store pending user data for OTP verification
                HttpContext.Session.SetString("Pending_UserName", response.UserName ?? loginUserDto.UserName);
                HttpContext.Session.SetString("Pending_UserRole", response.UserRoles ?? "Customer");
                HttpContext.Session.SetString("Pending_UserToken", response.UserToken ?? "");
                HttpContext.Session.SetString("Pending_Action", "Login");

                // Generate OTP and show verification page
                var otp = OtpHelper.GenerateAndStoreOtp(HttpContext.Session, 6, 5);
                TempData["GeneratedOtp"] = otp;
                TempData["OtpAction"] = "Login";
                TempData["UserEmail"] = loginUserDto.UserEmail;

                return RedirectToAction("VerifyOtp");
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
                // Store pending user data for OTP verification
                HttpContext.Session.SetString("Pending_UserName", response.UserName ?? registerUserDto.UserName);
                HttpContext.Session.SetString("Pending_UserRole", response.UserRoles ?? "Customer");
                HttpContext.Session.SetString("Pending_UserToken", response.UserToken ?? "");
                HttpContext.Session.SetString("Pending_Action", "Register");

                // Generate OTP and show verification page
                var otp = OtpHelper.GenerateAndStoreOtp(HttpContext.Session, 6, 5);
                TempData["GeneratedOtp"] = otp;
                TempData["OtpAction"] = "Register";
                TempData["UserEmail"] = registerUserDto.UserEmail;

                return RedirectToAction("VerifyOtp");

                //AddUserCreadentials(response);
                //return RedirectToAction("Index", "Home");
            }
            ViewBag.ErrorMessage = response.ErrorMessage == string.Empty ? "Registration failed." : response.ErrorMessage;
            return View(registerUserDto);
        }

        [HttpGet]
        public IActionResult VerifyOtp()
        {
            var otp = TempData["GeneratedOtp"] as string;
            var action = TempData["OtpAction"] as string;
            var email = TempData["UserEmail"] as string;

            if (string.IsNullOrEmpty(otp))
            {
                // Check if there's still an OTP in session
                var sessionOtp = HttpContext.Session.GetString("OTP_Code");
                if (string.IsNullOrEmpty(sessionOtp))
                {
                    return RedirectToAction("Login");
                }
                otp = sessionOtp;
            }

            var model = new OtpVerificationDto
            {
                PendingAction = action ?? HttpContext.Session.GetString("Pending_Action")
            };

            ViewBag.GeneratedOtp = otp;
            ViewBag.OtpAction = action;
            ViewBag.UserEmail = email;
            ViewBag.RemainingSeconds = OtpHelper.GetRemainingSeconds(HttpContext.Session);

            return View(model);
        }

        [HttpPost]
        public IActionResult VerifyOtp(OtpVerificationDto model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.GeneratedOtp = HttpContext.Session.GetString("OTP_Code");
                ViewBag.RemainingSeconds = OtpHelper.GetRemainingSeconds(HttpContext.Session);
                return View(model);
            }

            var (isValid, errorMessage) = OtpHelper.ValidateOtp(HttpContext.Session, model.OtpCode);

            if (!isValid)
            {
                ViewBag.ErrorMessage = errorMessage;
                ViewBag.GeneratedOtp = HttpContext.Session.GetString("OTP_Code");
                ViewBag.RemainingSeconds = OtpHelper.GetRemainingSeconds(HttpContext.Session);
                return View(model);
            }

            // OTP is valid - complete the login/registration
            var userName = HttpContext.Session.GetString("Pending_UserName");
            var userRole = HttpContext.Session.GetString("Pending_UserRole");
            var userToken = HttpContext.Session.GetString("Pending_UserToken");

            // Clear pending data
            HttpContext.Session.Remove("Pending_UserName");
            HttpContext.Session.Remove("Pending_UserRole");
            HttpContext.Session.Remove("Pending_UserToken");
            HttpContext.Session.Remove("Pending_Action");

            // Set user credentials
            HttpContext.Session.SetString("UserName", userName ?? "");
            HttpContext.Session.SetString("UserRole", userRole ?? "");
            HttpContext.Session.SetString("UserToken", userToken ?? "");

            TempData["UserName"] = userName;
            TempData["UserRole"] = userRole;
            TempData["SuccessMessage"] = "Verification successful! Welcome aboard.";

            // Redirect based on role
            if (userRole == "Admin")
            {
                return RedirectToAction("Index", "Admin");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult ResendOtp()
        {
            var pendingAction = HttpContext.Session.GetString("Pending_Action");

            if (string.IsNullOrEmpty(pendingAction))
            {
                return Json(new { success = false, message = "No pending verification found." });
            }

            // Generate new OTP
            var otp = OtpHelper.GenerateAndStoreOtp(HttpContext.Session, 6, 5);

            return Json(new
            {
                success = true,
                otp = otp,
                message = "New OTP generated successfully.",
                expirationMinutes = 5
            });
        }

        [HttpGet]
        public IActionResult CancelOtp()
        {
            // Clear all OTP and pending data
            OtpHelper.ClearOtp(HttpContext.Session);
            HttpContext.Session.Remove("Pending_UserName");
            HttpContext.Session.Remove("Pending_UserRole");
            HttpContext.Session.Remove("Pending_UserToken");
            HttpContext.Session.Remove("Pending_Action");

            return RedirectToAction("Login");
        }

        [HttpGet]
        public async Task<IActionResult> UserProfile(string userName)
        {
            if (HttpContext.Session.GetString("UserRole") == null || HttpContext.Session.GetString("UserRole") != "Customer")
                return Unauthorized("You have no authorization to access this page, get your authorization first and get back again");

            var userDto = new UserDto();
            userDto = await _apiUsersService.GetUserByNameAsync(userName);

            GetUserCredentials();
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
        public async Task<IActionResult> UserCart()
        {
            if (HttpContext.Session.GetString("UserRole") == null || HttpContext.Session.GetString("UserRole") != "Customer")
                return Unauthorized("You have no authorization to access this page, get your authorization first and get back again");

            // get user cart items from API and pass them to the view
            var userName = HttpContext.Session.GetString("UserName");
            var userCarts = await _apiCartsService.GetCartByUserNameAsync(userName!);

            return View(userCarts);
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

            HttpContext.Session.SetString("UserRole", loginResponseDto.UserRoles ?? "");
            TempData["UserRole"] = loginResponseDto.UserRoles;

            HttpContext.Session.SetString("UserToken", loginResponseDto.UserToken ?? "");
            TempData["UserToken"] = loginResponseDto.UserToken;

            HttpContext.Session.SetInt32("UserId", loginResponseDto.UserId);
            TempData["UserId"] = loginResponseDto.UserId;
        }
        private void RemoveUserCreadentials()
        {
            HttpContext.Session.SetString("UserName", "");
            TempData["UserName"] = "";

            HttpContext.Session.SetString("UserRole", "");
            TempData["UserRole"] = "";

            HttpContext.Session.SetString("UserToken", "");
            TempData["UserToken"] = "";

            HttpContext.Session.SetInt32("UserId", 0);
            TempData["UserId"] = 0;
        }
        private void GetUserCredentials()
        {
            TempData["UserName"] = HttpContext.Session.GetString("UserName");
            TempData["UserRole"] = HttpContext.Session.GetString("UserRole");
            TempData["UserToken"] = HttpContext.Session.GetString("UserToken");
            TempData["UserId"] = HttpContext.Session.GetInt32("UserId");
        }
    }
}
