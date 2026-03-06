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
