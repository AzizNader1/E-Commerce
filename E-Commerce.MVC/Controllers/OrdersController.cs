using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.MVC.Controllers
{
    public class OrdersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
