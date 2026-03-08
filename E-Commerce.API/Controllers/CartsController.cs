using E_Commerce.API.DTOs.CartDTOs;
using E_Commerce.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartService cartService;
        public CartsController(ICartService cartService)
        {
            this.cartService = cartService;
        }

        /// <summary>
        /// Gets a list of all carts in the system. The method retrieves all cart records from the database and returns them as a response. If any exceptions occur during the retrieval process, a bad request response is returned with the error message.
        /// </summary>
        /// <remarks>
        /// The method is designed to handle the retrieval of cart records in the system. It ensures that any issues encountered during the retrieval process are properly communicated back to the client through appropriate HTTP responses. If the retrieval is successful, it returns an Ok response containing the list of carts. If any exceptions occur, it returns a BadRequest response with details about the error.
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>
        /// An IActionResult containing the list of carts if the retrieval is successful. If an exception occurs during the retrieval process, it returns a BadRequest response with the error message.
        /// </returns>
        [HttpGet]
        public IActionResult GetAllCarts()
        {
            try
            {
                var carts = cartService.GetAllCarts();
                return Ok(carts);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Gets a cart from the system based on its unique identifier (ID). The method takes an integer ID as a parameter and attempts to retrieve the corresponding cart record from the database. If the retrieval is successful, it returns an Ok response containing the cart data. If any exceptions occur during the retrieval process, a bad request response is returned with the error message.
        /// </summary>
        /// <remarks>
        /// The method is designed to handle the retrieval of cart records in the system. It ensures that any issues encountered during the retrieval process are properly communicated back to the client through appropriate HTTP responses. If the provided ID does not correspond to an existing cart or if any other error occurs, the client will receive a BadRequest response with details about the error.
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>
        /// An IActionResult containing the cart data if the retrieval is successful. If an exception occurs during the retrieval process, it returns a BadRequest response with the error message.
        /// </returns>
        [HttpGet("{id}")]
        public IActionResult GetCartById(int id)
        {
            try
            {
                var cart = cartService.GetCartById(id);
                return Ok(cart);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Adds a new cart to the system based on the provided CreateCartDto object. The method takes a CreateCartDto as input, which contains the necessary information to create a new cart. It attempts to add the cart to the database and returns a Created response if successful. If any exceptions occur during the addition process, a bad request response is returned with the error message.
        /// </summary>
        /// <remarks>
        /// The method is designed to handle the creation of new cart records in the system. It ensures that any issues encountered during the addition process are properly communicated back to the client through appropriate HTTP responses. If the provided CreateCartDto does not contain valid data or if any other error occurs, the client will receive a BadRequest response with details about the error.
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>
        /// An IActionResult indicating the outcome of the add operation. If the cart is successfully added, it returns a Created response. If an exception occurs during the addition process, it returns a BadRequest response with the error message.
        /// </returns>
        [HttpPost]
        public IActionResult AddCart([FromBody] CreateCartDto createCartDto)
        {
            try
            {
                cartService.AddCart(createCartDto);
                return Created();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Updates an existing cart in the system based on the provided CartDto object. The method takes a CartDto as input, which contains the updated information for the cart. It attempts to update the corresponding cart record in the database with the new data. If the update is successful, it returns a NoContent response. If any exceptions occur during the update process, a bad request response is returned with the error message.
        /// </summary>
        /// <remarks>
        /// The method is designed to handle the updating of cart records in the system. It ensures that any issues encountered during the update process are properly communicated back to the client through appropriate HTTP responses. If the provided CartDto does not correspond to an existing cart or if any other error occurs, the client will receive a BadRequest response with details about the error.
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>
        /// An IActionResult indicating the outcome of the update operation. If the cart is successfully updated, it returns a NoContent response. If an exception occurs during the update process, it returns a BadRequest response with the error message.
        /// </returns>
        [HttpPut]
        public IActionResult UpdateCart([FromBody] CartDto cartDto)
        {
            try
            {
                cartService.UpdateCart(cartDto);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Deletes a cart from the system based on its unique identifier (ID). The method takes an integer ID as a parameter and attempts to remove the corresponding cart record from the database. If the deletion is successful, it returns a NoContent response. If any exceptions occur during the deletion process, a bad request response is returned with the error message.
        /// </summary>
        /// <remarks>
        /// The method is designed to handle the deletion of cart records in the system. It ensures that any issues encountered during the deletion process are properly communicated back to the client through appropriate HTTP responses. If the provided ID does not correspond to an existing cart or if any other error occurs, the client will receive a BadRequest response with details about the error.
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>
        /// An IActionResult indicating the outcome of the delete operation. If the cart is successfully deleted, it returns a NoContent response. If an exception occurs during the deletion process, it returns a BadRequest response with the error message.
        /// </returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteCart(int id)
        {
            try
            {
                cartService.DeleteCart(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Gets all carts associated with a specific user based on their unique identifier (ID).
        /// </summary>
        /// <remarks>
        /// The method retrieves all cart records from the database that are linked to the user identified by the provided ID. If any exceptions occur during the retrieval process, a bad request response is returned with the error message.
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>
        /// An IActionResult containing the list of carts associated with the specified user ID if the retrieval is successful. If an exception occurs, it returns a BadRequest response with the error message.
        /// </returns>
        [HttpGet("{userId}")]
        public IActionResult GetAllCartsByUserIdAsync(int userId)
        {
            try
            {
                var carts = cartService.GetAllCartsByUserId(userId);
                return Ok(carts);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Gets all carts associated with a specific user based on their username.
        /// </summary>
        /// <remarks>
        /// The method retrieves all cart records from the database that are linked to the user identified by the provided username. If any exceptions occur during the retrieval process, a bad request response is returned with the error message.
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>
        /// An IActionResult containing the list of carts associated with the specified username if the retrieval is successful. If an exception occurs, it returns a BadRequest response with the error message.
        /// </returns>
        [HttpGet("{userName}")]
        public IActionResult GetAllCartsByUserNameAsync(string userName)
        {
            try
            {
                var carts = cartService.GetAllCartsByUserName(userName);
                return Ok(carts);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
