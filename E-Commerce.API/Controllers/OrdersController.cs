using E_Commerce.API.DTOs.OrderDTOs;
using E_Commerce.API.Services;
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

        /// <summary>
        /// Gets a list of all orders in the system. The method retrieves all order records from the database and returns them as a response. If any exceptions occur during the retrieval process, a bad request response is returned with the error message.
        /// </summary>
        /// <remarks>
        /// The method processes the get request to retrieve all orders. If the retrieval is successful, it returns an OK response with the list of orders. If any exceptions occur during the retrieval process, it returns a BadRequest response with an appropriate error message.
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>
        /// An IActionResult containing the list of orders if the retrieval is successful. If an exception occurs, it returns a BadRequest response with the error message.
        /// </returns>
        [HttpGet]
        public IActionResult GetAllOrders()
        {
            try
            {
                var orders = orderService.GetAllOrders();
                return Ok(orders);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Gets an order by its unique identifier (ID). The method takes an integer ID as a parameter and attempts to retrieve the corresponding order from the database using the order service. If the order is found, it returns the order information. If any exceptions occur during the retrieval process, a bad request response is returned with the error message.
        /// </summary>
        /// <remarks>
        /// The method processes the get request to retrieve an order based on its unique identifier (ID). It validates the input ID and interacts with the order service to fetch the relevant order. If the retrieval is successful, it returns the order information. If any exceptions occur during the retrieval process, a bad request response is returned with the error message.
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>
        /// An IActionResult containing the order information if the retrieval is successful. If an exception occurs, it returns a BadRequest response with the error message.
        /// </returns>
        [HttpGet("{id}")]
        public IActionResult GetOrderById(int id)
        {
            try
            {
                var order = orderService.GetOrderById(id);
                return Ok(order);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Adds a new order to the system based on the provided order data. The method takes a CreateOrderDto object as input, which contains the necessary information to create a new order. It attempts to add the order to the database using the order service. If the addition is successful, it returns a Created response. If any exceptions occur during the addition process, it returns a BadRequest response with an appropriate error message.
        /// </summary>
        /// <remarks>
        /// The method processes the add request to create a new order in the system based on the provided order data. It validates the input CreateOrderDto object and interacts with the order service to perform the addition. If the addition is successful, it returns a Created response. If any exceptions occur during the addition process, it returns a BadRequest response with an appropriate error message.
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>
        /// An IActionResult indicating the result of the add operation. If the order is successfully added, it returns a Created response. If an exception occurs, it returns a BadRequest response with the error message.
        /// </returns>
        [HttpPost]
        public IActionResult AddOrder([FromBody] CreateOrderDto createOrderDto)
        {
            try
            {
                orderService.AddOrder(createOrderDto);
                return Created();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Updates an existing order in the system based on the provided order data. The method takes an OrderDto object as input, which contains the updated information for the order. It attempts to update the corresponding order in the database using the order service. If the update is successful, it returns a NoContent response. If any exceptions occur during the update process, it returns a BadRequest response with an appropriate error message.
        /// </summary>
        /// <remarks>
        /// The method processes the update request to modify an existing order in the system based on the provided order data. It validates the input OrderDto object and interacts with the order service to perform the update. If the update is successful, it returns a NoContent response. If any exceptions occur during the update process, it returns a BadRequest response with an appropriate error message.
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>
        /// An IActionResult indicating the result of the update operation. If the order is successfully updated, it returns a NoContent response. If an exception occurs, it returns a BadRequest response with the error message.
        /// </returns>
        [HttpPut]
        public IActionResult UpdateOrder([FromBody] OrderDto orderDto)
        {
            try
            {
                orderService.UpdateOrder(orderDto);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Deletes an order from the system based on its unique identifier (ID). The method takes an integer parameter representing the order ID and attempts to delete the corresponding order from the database using the order service. If the deletion is successful, it returns a NoContent response. If any exceptions occur during the deletion process, it returns a BadRequest response with an appropriate error message.
        /// </summary>
        /// <remarks>
        /// The method processes the delete request to remove an order from the system based on its unique identifier (ID). It validates the input order ID and interacts with the order service to perform the deletion. If the deletion is successful, it returns a NoContent response. If any exceptions occur during the deletion process, it returns a BadRequest response with an appropriate error message.
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>
        /// An IActionResult indicating the result of the delete operation. If the order is successfully deleted, it returns a NoContent response. If an exception occurs, it returns a BadRequest response with the error message.
        /// </returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            try
            {
                orderService.DeleteOrder(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Gets a list of all orders associated with a specific user, identified by their unique identifier (ID). The method takes an integer parameter representing the user ID and retrieves the corresponding orders from the database using the order service. If the retrieval is successful, it returns an OK response with the list of orders. If any exceptions occur during the retrieval process, it returns a BadRequest response with an appropriate error message.
        /// </summary>
        /// <remarks>
        /// The method processes the get request to retrieve all orders for a specific user based on their unique identifier (ID). It validates the input user ID and interacts with the order service to fetch the relevant orders. If the retrieval is successful, it returns an OK response with the list of orders. If any exceptions occur during the retrieval process, it returns a BadRequest response with an appropriate error message.
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>
        /// An IActionResult containing the list of orders associated with the specified user ID if the retrieval is successful. If an exception occurs, it returns a BadRequest response with the error message.
        /// </returns>
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


        /// <summary>
        /// Gets a list of all orders associated with a specific user, identified by their username. The method takes a string parameter representing the username and retrieves the corresponding orders from the database using the order service. If the retrieval is successful, it returns an OK response with the list of orders. If any exceptions occur during the retrieval process, it returns a BadRequest response with an appropriate error message.
        /// </summary>
        /// <remarks>
        /// The method processes the get request to retrieve all orders for a specific user based on their username. It validates the input username and interacts with the order service to fetch the relevant orders. If the retrieval is successful, it returns an OK response with the list of orders. If any exceptions occur during the retrieval process, it returns a BadRequest response with an appropriate error message.
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>
        /// An IActionResult containing the list of orders associated with the specified username if the retrieval is successful. If an exception occurs, it returns a BadRequest response with the error message.
        /// </returns>
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
