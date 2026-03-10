using E_Commerce.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IApiProductsService _productService;

        public HomeController(IApiProductsService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                TempData["UserName"] = "";
                TempData["UserRole"] = "";
                TempData["UserToken"] = "";
            }
            else
            {
                TempData["UserName"] = HttpContext.Session.GetString("UserName");
                TempData["UserRole"] = HttpContext.Session.GetString("UserRole");
                TempData["UserToken"] = HttpContext.Session.GetString("UserToken");
            }
            var products = await _productService.GetAllProductsAsync();
            return View(products);
        }
    }
}
