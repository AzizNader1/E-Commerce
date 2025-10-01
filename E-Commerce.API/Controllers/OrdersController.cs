using E_Commerce.API.DTOs.OrderDTOs;
using E_Commerce.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService orderService;
        public OrdersController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet]
        public IActionResult GetAllOrders()
        {
            try
            {
                var orders = orderService.GetAllOrdersAsync();
                return Ok(orders);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderById(int id)
        {
            try
            {
                var order = orderService.GetOrderByIdAsync(id);
                return Ok(order);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult AddOrder([FromBody] CreateOrderDto createOrderDto)
        {
            try
            {
                orderService.AddOrderAsync(createOrderDto);
                return Created();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateOrder([FromBody] OrderDto orderDto)
        {
            try
            {
                orderService.UpdateOrderAsync(orderDto);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            try
            {
                orderService.DeleteOrderAsync(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{userId}")]
        public IActionResult GetAllOrdersByUserId(int userId)
        {
            try
            {
                var orders = orderService.GetAllOrdersByUserId(userId);
                return Ok(orders);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{userName}")]
        public IActionResult GetAllOrdersByUserName(string userName)
        {
            try
            {
                var orders = orderService.GetAllOrdersByUserName(userName);
                return Ok(orders);
            }
            catch (Exception e)
            { 
                return BadRequest(e.Message);
            }

        }
    }
}
