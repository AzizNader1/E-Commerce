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

        /// <summary>
        /// Gets a list of all products in the system.
        /// </summary>
        /// <remarks>
        /// the method retrieves all product records from the database and returns them as a response. If any exceptions occur during the retrieval process, a bad request response is returned with the error message.
        /// </remarks>
        /// <returns>
        /// Returns an IActionResult containing the list of products if the retrieval is successful.
        /// </returns>
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

        /// <summary>
        /// Gets a product by its unique identifier.
        /// </summary>
        /// <remarks>
        /// The method retrieves a product record from the database based on the provided ID. If the product is found, it returns the product details as a response. If any exceptions occur during the retrieval process, a bad request response is returned with the error message.
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>
        /// Returns an IActionResult containing the product details if the retrieval is successful.
        /// </returns>
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

        /// <summary>
        /// Adds a new product to the system.
        /// </summary>
        /// <remarks>
        /// The method accepts a CreateProductDto object containing the product details and an optional product image file. It creates a new product record in the database based on the provided information. If the product is successfully created, it returns the created product details as a response. If any exceptions occur during the creation process, a bad request response is returned with the error message.
        /// </remarks>
        /// <param name="createProductDto"></param>
        /// <param name="productImage"></param>
        /// <returns>
        /// Returns an IActionResult containing the created product details if the creation is successful.
        /// </returns>
        [HttpPost]
        public IActionResult AddProduct([FromBody] CreateProductDto createProductDto, IFormFile productImage)
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

        /// <summary>
        /// Updates an existing product in the system.
        /// </summary>
        /// <remarks>
        /// The method accepts an UpdateProductDto object containing the updated product details and an optional product image file. It updates the existing product record in the database based on the provided information. If the product is successfully updated, it returns the updated product details as a response. If any exceptions occur during the update process, a bad request response is returned with the error message.
        /// </remarks>
        /// <param name="updateProductDto"></param>
        /// <param name="productImage"></param>
        /// <returns>
        /// Returns an IActionResult containing the updated product details if the update is successful.
        /// </returns>
        [HttpPut]
        public IActionResult UpdateProduct([FromBody] UpdateProductDto updateProductDto, IFormFile productImage)
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

        /// <summary>
        /// Deletes a product from the system based on the provided ID.
        /// </summary>
        /// <remarks>
        /// The method accepts an integer ID as a parameter and deletes the corresponding product record from the database. If the product is successfully deleted, it returns a success message as a response. If any exceptions occur during the deletion process, a bad request response is returned with the error message.
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>
        /// An IActionResult indicating the outcome of the delete operation. Returns an OK result with a success message if the deletion is successful; otherwise, returns a BadRequest result with an error message if an exception occurs during the deletion process.
        /// </returns>
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


        /// <summary>
        /// Gets a list of products based on the provided category ID.
        /// </summary>
        /// <remarks>
        /// The method accepts an integer category ID as a parameter and retrieves all product records from the database that belong to the specified category. If the products are found, it returns the list of products as a response. If any exceptions occur during the retrieval process, a bad request response is returned with the error message.
        /// </remarks>
        /// <param name="categoryId"></param>
        /// <returns>
        /// An IActionResult containing the list of products that belong to the specified category if the retrieval is successful. If any exceptions occur during the retrieval process, it returns a BadRequest result with an error message.
        /// </returns>
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

        /// <summary>
        /// Gets a list of products based on the provided category name.
        /// </summary>
        /// <remarks>
        /// The method accepts a category name as a parameter and retrieves all product records from the database that belong to the specified category. If the products are found, it returns the list of products as a response. If any exceptions occur during the retrieval process, a bad request response is returned with the error message.
        /// </remarks>
        /// <param name="categoryName"></param>
        /// <returns>
        /// An IActionResult containing the list of products that belong to the specified category if the retrieval is successful. If any exceptions occur during the retrieval process, it returns a BadRequest result with an error message.
        /// </returns>
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
