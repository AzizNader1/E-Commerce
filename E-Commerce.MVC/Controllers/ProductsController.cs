using E_Commerce.MVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.MVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IApiProductsService _productService;

        public ProductsController(IApiProductsService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(int productId, int quantity = 1)
        {
            // The user must be signed in because of the [Authorize] attribute.
            // When we implement the real Cart logic, we will call ApiCartService here

            // For now, redirect back to the home page or cart page
            TempData["SuccessMessage"] = "Product added to cart successfully!";
            return RedirectToAction("Index", "Home");
        }
    }
}
