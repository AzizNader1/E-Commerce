using E_Commerce.API.DTOs.ProductDTOs;
using E_Commerce.API.Models;
using E_Commerce.API.Services;
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
                var products = productService.GetAllProducts();
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
                var product = productService.GetProductById(id);
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
                var createdProduct = productService.AddProduct(createProductDto, productImage);
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
                var updatedProduct = productService.UpdateProduct(updateProductDto, productImage);
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
                productService.DeleteProduct(id);
                return Ok("this product deleted successfully");
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
        public IActionResult GetAllProductsByCategoryName(CategoriesCollections categoryName)
        {
            try
            {
                var products = productService.GetAllProductsByCategoryName(categoryName.ToString());
                return Ok(products);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
