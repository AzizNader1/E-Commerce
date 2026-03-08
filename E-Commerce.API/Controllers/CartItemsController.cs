using E_Commerce.API.DTOs.CartItemDTOs;
using E_Commerce.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CartItemsController : ControllerBase
    {
        private readonly ICartItemService cartItemService;
        public CartItemsController(ICartItemService cartItemService)
        {
            this.cartItemService = cartItemService;
        }

        /// <summary>
        /// Gets a list of all cart items in the system.
        /// </summary>
        /// <remarks>
        /// The method retrieves all cart item records from the database and returns them as a response. If any exceptions occur during the retrieval process, a bad request response is returned with the error message.
        /// </remarks>
        /// <returns>
        /// An IActionResult containing the list of cart items if the retrieval is successful. If an exception occurs, it returns a BadRequest response with the error message.
        /// </returns>
        [HttpGet]
        public IActionResult GetAllCartItems()
        {
            try
            {
                var cartItems = cartItemService.GetAllCartItems();
                return Ok(cartItems);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Gets a specific cart item by its unique identifier.
        /// </summary>
        /// <remarks>
        /// The method retrieves a cart item record from the database based on the provided unique identifier (id). If the cart item is found, it returns the cart item details as a response. If any exceptions occur during the retrieval process, a bad request response is returned with the error message.
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>
        /// An IActionResult containing the cart item details if the retrieval is successful. If an exception occurs, it returns a BadRequest response with the error message.
        /// </returns>
        [HttpGet("{id}")]
        public IActionResult GetCartItemById(int id)
        {
            try
            {
                var cartItem = cartItemService.GetCartItemById(id);
                return Ok(cartItem);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Adds a new cart item to the system based on the provided data transfer object (DTO).
        /// </summary>
        /// <remarks>
        /// The method takes a CreateCartItemDto object as input, which contains the necessary information to create a new cart item record in the database. If the cart item is successfully added, it returns a Created response. If any exceptions occur during the addition process, a bad request response is returned with the error message.
        /// </remarks>  
        /// <param name="createCartItemDto"></param>
        /// <returns>
        /// An IActionResult indicating the result of the add operation. If the cart item is successfully added, it returns a Created response. If an exception occurs, it returns a BadRequest response with the error message.
        /// </returns>
        [HttpPost]
        public IActionResult AddCartItem([FromBody] CreateCartItemDto createCartItemDto)
        {
            try
            {
                cartItemService.AddCartItem(createCartItemDto);
                return Created();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// <summary>
        /// Updates an existing cart item in the system based on the provided data transfer object (DTO).
        /// </summary>
        /// <remarks>
        /// The method takes a CartItemDto object as input, which contains the necessary information to update an existing cart item record in the database. If the cart item is successfully updated, it returns a NoContent response. If any exceptions occur during the update process, a bad request response is returned with the error message.
        /// </remarks>
        /// <param name="cartItemDto"></param>
        /// <returns>
        /// An IActionResult indicating the result of the update operation. If the cart item is successfully updated, it returns a NoContent response. If an exception occurs, it returns a BadRequest response with the error message.
        /// </returns>
        [HttpPut]
        public IActionResult UpdateCartItem([FromBody] CartItemDto cartItemDto)
        {
            try
            {
                cartItemService.UpdateCartItem(cartItemDto);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Deletes a cart item from the system based on the provided unique identifier (id).
        /// </summary>
        /// <remarks>
        /// The method takes an integer id as a parameter and deletes the corresponding cart item record from the database. If the cart item is successfully deleted, it returns a NoContent response. If any exceptions occur during the deletion process, a bad request response is returned with the error message.
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>
        /// An IActionResult indicating the result of the delete operation. If the cart item is successfully deleted, it returns a NoContent response. If an exception occurs, it returns a BadRequest response with the error message.
        /// </returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteCartItem(int id)
        {
            try
            {
                cartItemService.DeleteCartItem(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Gets a list of cart items associated with a specific cart based on the provided cart identifier (cartId).
        /// </summary>
        /// <remarks>
        /// The method takes an integer cartId as a parameter and retrieves all cart item records from the database that are associated with the specified cart. If the cart items are found, it returns the list of cart items as a response. If any exceptions occur during the retrieval process, a bad request response is returned with the error message.
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>
        /// An IActionResult containing the list of cart items associated with the specified cart if the retrieval is successful. If an exception occurs, it returns a BadRequest response with the error message.
        /// </returns>
        [HttpGet("{cartId}")]
        public IActionResult GetCartItemsByCartIdAsync(int cartId)
        {
            try
            {
                var cartItems = cartItemService.GetCartItemsByCartId(cartId);
                return Ok(cartItems);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
