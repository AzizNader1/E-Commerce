using E_Commerce.MVC.DTOs.OrderDTOs;
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
        public async Task<IActionResult> Index()
        {
            // here user can see all his/her orders, and click on each order to see the details of it and if he wants to cancel it,
            // he can click on cancel button to cancel the order if it is not shipped yet
            var orderProductDto = await _apiOrdersService.GetOrdersByUserNameAsync(HttpContext.Session.GetString("UserName")!);
            if (orderProductDto == null)
            {
                TempData["ErrorMessage"] = "No orders found for the current user.";
            }

            return View(orderProductDto);
        }

        [HttpGet]
        public async Task<IActionResult> Checkout(int orderId)
        {
            // Implement the logic to retrieve the cart items for the current user and pass it to the checkout view
            var orderDetails = await _apiOrdersService.GetOrderByIdAsync(orderId);
            if (orderDetails == null)
            {
                TempData["ErrorMessage"] = "Order not found.";
                return RedirectToAction("Index");
            }
            return View(orderDetails);
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(CheckoutViewModel checkoutViewModel)
        {
            // Implement the logic to place the order and update the order status accordingly
            var createOrderDto = new CreateOrderDto
            {
                OrderDate = DateTime.Now,
                UserId = checkoutViewModel.User!.UserId,
                TotalAmount = checkoutViewModel.Cart!.TotalPrice
            };
            var result = await _apiOrdersService.CreateOrderAsync(createOrderDto);
            if (result != null)
            {
                TempData["SuccessMessage"] = "Order placed successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to place the order. Please try again.";
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> OrderDetails(int orderId)
        {
            // Implement the logic to retrieve the order details using the given orderId and pass it to the view
            var orderDetails = await _apiOrdersService.GetOrderByIdAsync(orderId);
            if (orderDetails == null)
            {
                TempData["ErrorMessage"] = "Order not found.";
                return RedirectToAction("Index");
            }

            return View(orderDetails);
        }

        [HttpPost]
        public async Task<IActionResult> CancleOrder(int orderId)
        {
            // Implement the logic to cancel the order with the given orderId
            var result = await _apiOrdersService.UpdateOrderStatusAsync(orderId, Models.OrderStatus.Cancelled);
            if (result)
            {
                TempData["SuccessMessage"] = "Order cancelled successfully.";

            }
            else
            {
                TempData["ErrorMessage"] = "Failed to cancel the order. Please try again.";
            }

            return RedirectToAction("Index");
        }
    }
}
