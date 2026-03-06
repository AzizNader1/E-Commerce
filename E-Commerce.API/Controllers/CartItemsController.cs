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
