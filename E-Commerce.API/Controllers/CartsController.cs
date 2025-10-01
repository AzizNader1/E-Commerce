using E_Commerce.API.DTOs.CartDTOs;
using E_Commerce.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartService cartService;
        public CartsController(ICartService cartService)
        {
            this.cartService = cartService;
        }

        [HttpGet]
        public IActionResult GetAllCarts()
        {
            try
            {
                var carts = cartService.GetAllCartsAsync();
                return Ok(carts);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetCartById(int id)
        {
            try
            {
                var cart = cartService.GetCartByIdAsync(id);
                return Ok(cart);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult AddCart([FromBody] CreateCartDto createCartDto)
        {
            try
            {
                cartService.AddCartAsync(createCartDto);
                return Created();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateCart([FromBody] CartDto cartDto)
        {
            try
            {
                cartService.UpdateCartAsync(cartDto);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCart(int id)
        {
            try
            {
                cartService.DeleteCartAsync(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
