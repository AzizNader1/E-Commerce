using E_Commerce.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.MVC.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IApiUsersService _apiUsersService;
        private readonly IApiOrdersService _apiOrdersService;
        private readonly IApiProductsService _apiProductsService;
        public OrdersController(IApiUsersService apiUsersService,
            IApiProductsService apiProductsService,
            IApiOrdersService apiOrdersService)
        {
            _apiOrdersService = apiOrdersService;
            _apiUsersService = apiUsersService;
            _apiProductsService = apiProductsService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            // here user can see all his/her orders, and click on each order to see the details of it and if he wants to cancel it,
            // he can click on cancel button to cancel the order if it is not shipped yet

            return View();
        }

        [HttpGet]
        public IActionResult OrderDetails(int orderId)
        {
            // Implement the logic to retrieve the order details using the given orderId and pass it to the view
            return View();
        }

        [HttpPost]
        public IActionResult CancleOrder(int orderId)
        {
            // Implement the logic to cancel the order with the given orderId

            return View();
        }
    }
}
