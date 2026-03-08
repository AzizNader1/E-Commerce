using E_Commerce.API.DTOs.OrderItemDTOs;
using E_Commerce.API.Services;
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

        /// <summary>
        /// Gets a list of all order items in the system. The method retrieves all order item records from the database and returns them as a response. If any exceptions occur during the retrieval process, a bad request response is returned with the error message.
        /// </summary>
        /// <remarks>
        /// The method processes the get request to retrieve all order items. If the retrieval is successful, it returns an OK response with the list of order items. If any exceptions occur during the retrieval process, it returns a BadRequest response with an appropriate error message.
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>
        /// An IActionResult containing the list of order items if the retrieval is successful. If an exception occurs, it returns a BadRequest response with the error message.
        /// </returns>
        [HttpGet]
        public IActionResult GetAllOrderItems()
        {
            try
            {
                var orderItems = orderItemService.GetAllOrderItems();
                return Ok(orderItems);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Gets an order item based on the provided unique identifier (ID). The method takes an integer ID as a parameter and retrieves the corresponding order item from the database using the order item service. If the retrieval is successful, it returns an OK response with the order item. If any exceptions occur during the retrieval process, a bad request response is returned with the error message.
        /// </summary>
        /// <remarks>
        /// The method processes the get request to retrieve an order item based on the provided ID. If the retrieval is successful, it returns an OK response with the order item. If any exceptions occur during the retrieval process, it returns a BadRequest response with an appropriate error message.
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>
        /// An IActionResult containing the order item associated with the specified ID if the retrieval is successful. If an exception occurs, it returns a BadRequest response with the error message.
        /// </returns>
        [HttpGet("{id}")]
        public IActionResult GetOrderItemById(int id)
        {
            try
            {
                var orderItem = orderItemService.GetOrderItemById(id);
                return Ok(orderItem);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Adds a new order item based on the provided order item data transfer object (DTO). The method takes a CreateOrderItemDto as a parameter and attempts to add the corresponding order item to the database using the order item service. If the addition is successful, it returns a Created response. If any exceptions occur during the addition process, a bad request response is returned with the error message.
        /// </summary>
        /// <remarks>
        /// The method processes the add request to create a new order item based on the provided order item DTO. If the addition is successful, it returns a Created response. If any exceptions occur during the addition process, it returns a BadRequest response with an appropriate error message.
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>
        /// An IActionResult indicating the result of the add operation. If the order item is successfully added, it returns a Created response. If an exception occurs, it returns a BadRequest response with the error message.
        /// </returns>
        [HttpPost]
        public IActionResult AddOrderItem([FromBody] CreateOrderItemDto createOrderItemDto)
        {
            try
            {
                orderItemService.AddOrderItem(createOrderItemDto);
                return Created();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Updates an existing order item based on the provided order item data transfer object (DTO). The method takes an OrderItemDto as a parameter and attempts to update the corresponding order item in the database using the order item service. If the update is successful, it returns a NoContent response. If any exceptions occur during the update process, a bad request response is returned with the error message.
        /// </summary>
        /// <remarks>
        /// The method processes the update request to modify an existing order item based on the provided order item DTO. If the update is successful, it returns a NoContent response. If any exceptions occur during the update process, it returns a BadRequest response with an appropriate error message.
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>
        /// An IActionResult indicating the result of the update operation. If the order item is successfully updated, it returns a NoContent response. If an exception occurs, it returns a BadRequest response with the error message.
        /// </returns>
        [HttpPut]
        public IActionResult UpdateOrderItem([FromBody] OrderItemDto orderItemDto)
        {
            try
            {
                orderItemService.UpdateOrderItem(orderItemDto);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Deletes an order item based on the provided unique identifier (ID). The method takes an integer ID as a parameter and attempts to delete the corresponding order item from the database using the order item service. If the deletion is successful, it returns a NoContent response. If any exceptions occur during the deletion process, a bad request response is returned with the error message.
        /// </summary>
        /// <remarks>
        /// The method processes the delete request to remove an order item based on the provided ID. If the deletion is successful, it returns a NoContent response. If any exceptions occur during the deletion process, it returns a BadRequest response with an appropriate error message.
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>
        /// An IActionResult indicating the result of the delete operation. If the order item is successfully deleted, it returns a NoContent response. If an exception occurs, it returns a BadRequest response with the error message.
        /// </returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteOrderItem(int id)
        {
            try
            {
                orderItemService.DeleteOrderItem(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Gets a list of order items associated with a specific order ID. The method takes an integer orderId as a parameter and retrieves the corresponding order items from the database using the order item service. If the retrieval is successful, it returns an OK response with the list of order items. If any exceptions occur during the retrieval process, a bad request response is returned with the error message.
        /// </summary>
        /// <remarks>
        /// The method processes the get request to retrieve order items based on the provided order ID. If the retrieval is successful, it returns an OK response with the list of order items. If any exceptions occur during the retrieval process, it returns a BadRequest response with an appropriate error message.
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>
        /// An IActionResult containing the list of order items associated with the specified order ID if the retrieval is successful. If an exception occurs, it returns a BadRequest response with the error message.
        /// </returns>
        [HttpGet("{orderId}")]
        public IActionResult GetOrderItemsByOrderIdAsync(int orderId)
        {
            try
            {
                var orderItems = orderItemService.GetOrderItemsByOrderId(orderId);
                return Ok(orderItems);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
