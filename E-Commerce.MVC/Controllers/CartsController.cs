using E_Commerce.MVC.DTOs.CartDTOs;
using E_Commerce.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.MVC.Controllers
{
    public class CartsController : Controller
    {
        private readonly IApiCartsService _apiCartsService;
        private readonly IApiProductsService _apiProductsService;
        private readonly IApiUsersService _apiUsersService;

        public CartsController(
            IApiCartsService cartsService,
            IApiProductsService productsService,
            IApiUsersService usersService)
        {
            _apiCartsService = cartsService;
            _apiProductsService = productsService;
            _apiUsersService = usersService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("UserRole") == null || HttpContext.Session.GetString("UserRole") != "Customer")
                return Unauthorized("You have no authorization to access this page, get your authorization first and get back again");

            var userName = HttpContext.Session.GetString("UserName");

            // Get user to get userId
            var user = await _apiUsersService.GetUserByNameAsync(userName);

            // Get cart by user name
            var cart = await _apiCartsService.GetCartByUserNameAsync(userName);

            if (cart == null)
            {
                cart = new CartShoppingDto
                {
                    UserId = user.UserId,
                    UserName = userName,
                    CartItems = new List<CartWithProductDto>()
                };
            }

            // Pass user info for TempData
            TempData["UserName"] = userName;
            TempData["UserRole"] = HttpContext.Session.GetString("UserRole");
            TempData["UserToken"] = HttpContext.Session.GetString("UserToken");

            return View(cart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveItem(int cartItemId)
        {
            if (HttpContext.Session.GetString("UserRole") == null || HttpContext.Session.GetString("UserRole") != "Customer")
                return Unauthorized("You have no authorization to access this page, get your authorization first and get back again");

            var result = await _apiCartsService.DeleteCartItemAsync(cartItemId);

            if (result)
            {
                TempData["SuccessMessage"] = "Item removed from cart.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to remove item from cart.";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClearCart()
        {
            if (HttpContext.Session.GetString("UserRole") == null || HttpContext.Session.GetString("UserRole") != "Customer")
                return Unauthorized("You have no authorization to access this page, get your authorization first and get back again");

            var cart = await _apiCartsService.GetCartByUserNameAsync(HttpContext.Session.GetString("UserName")!);
            if (cart != null)
            {
                var result = await _apiCartsService.ClearCartAsync(cart.CartId);
                if (result)
                {
                    TempData["SuccessMessage"] = "Cart cleared successfully.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to clear cart.";
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
