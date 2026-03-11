using E_Commerce.MVC.DTOs.CartItemDTOs;
using E_Commerce.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.MVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IApiProductsService _productService;
        private readonly IApiCartsService _apiCartsService;

        public ProductsController(IApiProductsService productService, IApiCartsService apiCartsService)
        {
            _productService = productService;
            _apiCartsService = apiCartsService;
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            TempData["UserName"] = HttpContext.Session.GetString("UserName");
            TempData["UserRole"] = HttpContext.Session.GetString("UserRole");
            TempData["UserToken"] = HttpContext.Session.GetString("UserToken");
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(int productId, int quantity = 1)
        {
            var hasCart = await _apiCartsService.GetCartByUserName(HttpContext.Session.GetString("UserName")!);
            if (!hasCart)
            {
                TempData["ErrorMessage"] = "Fails to add this product to your cart!";
                return RedirectToAction("Index", "Home");
            }

            var cartData = await _apiCartsService.GetCartByUserNameAsync(HttpContext.Session.GetString("UserName")!);
            if (cartData == null)
            {
                TempData["ErrorMessage"] = "Fails to add this product to your cart!";
                return RedirectToAction("Index", "Home");
            }

            var result = await _apiCartsService.AddCartItemAsync(new CreateCartItemDto
            {
                CartId = cartData.CartId,
                ProductId = productId,
                Quantity = quantity
            });

            if (result != null)
            {
                TempData["SuccessMessage"] = "Product added to cart successfully!";
                return RedirectToAction("Index", "Home");
            }

            TempData["ErrorMessage"] = "Fails to add this product to your cart!";
            return RedirectToAction("Index", "Home");

        }
    }
}
