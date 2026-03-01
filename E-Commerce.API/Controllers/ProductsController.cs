using E_Commerce.API.DTOs.ProductDTOs;
using E_Commerce.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;
        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            try
            {
                var products = productService.GetAllProductsAsync();
                return Ok(products);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            try
            {
                var product = productService.GetProductByIdAsync(id);
                return Ok(product);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult AddProduct([FromForm] CreateProductDto createProductDto, IFormFile productImage)
        {
            try
            {
                var createdProduct = productService.AddProductAsync(createProductDto, productImage);
                return Ok(createdProduct);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateProduct([FromForm] UpdateProductDto updateProductDto, IFormFile productImage)
        {
            try
            {
                var updatedProduct = productService.UpdateProductAsync(updateProductDto, productImage);
                return Ok(updatedProduct);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                productService.DeleteProductAsync(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{categoryId}")]
        public IActionResult GetAllProductsByCategoryId(int categoryId)
        {
            try
            {
                var products = productService.GetAllProductsByCategoryId(categoryId);
                return Ok(products);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{categoryName}")]
        public IActionResult GetAllProductsByCategoryName(string categoryName)
        {
            try
            {
                var products = productService.GetAllProductsByCategoryName(categoryName);
                return Ok(products);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
