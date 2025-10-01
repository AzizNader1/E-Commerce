using E_Commerce.API.DTOs.OrderItemDTOs;
using E_Commerce.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly IOrderItemService orderItemService;
        public OrderItemsController(IOrderItemService orderItemService)
        {
            this.orderItemService = orderItemService;
        }

        [HttpGet]
        public IActionResult GetAllOrderItems()
        {
            try
            {
                var orderItems = orderItemService.GetAllOrderItemsAsync();
                return Ok(orderItems);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderItemById(int id)
        {
            try
            {
                var orderItem = orderItemService.GetOrderItemByIdAsync(id);
                return Ok(orderItem);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult AddOrderItem([FromBody] CreateOrderItemDto createOrderItemDto)
        {
            try
            {
                orderItemService.AddOrderItemAsync(createOrderItemDto);
                return Created();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateOrderItem([FromBody] OrderItemDto orderItemDto)
        {
            try
            {
                orderItemService.UpdateOrderItemAsync(orderItemDto);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrderItem(int id)
        {
            try
            {
                orderItemService.DeleteOrderItemAsync(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{orderId}")]
        public IActionResult GetOrderItemsByOrderIdAsync(int orderId)
        {
            try
            {
                var orderItems = orderItemService.GetOrderItemsByOrderIdAsync(orderId);
                return Ok(orderItems);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
