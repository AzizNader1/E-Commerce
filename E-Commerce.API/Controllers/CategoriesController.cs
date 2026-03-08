using E_Commerce.API.DTOs.CategoryDTOs;
using E_Commerce.API.Models;
using E_Commerce.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        /// <summary>
        /// Gets a list of all categories in the system. The method retrieves all category records from the database and returns them as a response. If any exceptions occur during the retrieval process, a bad request response is returned with the error message.
        /// </summary>
        /// <remarks>
        /// The method processes the get request to retrieve all categories. If the retrieval is successful, it returns an OK response with the list of categories. If any exceptions occur during the retrieval process, it returns a BadRequest response with an appropriate error message.
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>
        /// An IActionResult containing the list of categories if the retrieval is successful. If an exception occurs, it returns a BadRequest response with the error message.
        /// </returns>
        [HttpGet]
        public IActionResult GetAllCategories()
        {
            try
            {
                var categories = categoryService.GetAllCategories();
                return Ok(categories);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Gets a category by its unique identifier (ID). The method takes an integer ID as a parameter and attempts to retrieve the corresponding category from the database using the category service. If the category is found, it returns the category information. If any exceptions occur during the retrieval process, a bad request response is returned with the error message.
        /// </summary>
        /// <remarks>
        /// The method validates the input ID and processes the get request accordingly. If the ID is valid and the category is successfully retrieved, it returns an OK response with the category information. If the ID is invalid or any exceptions occur during the retrieval process, it returns a BadRequest response with an appropriate error message.
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>
        /// An IActionResult indicating the result of the get operation. Returns an OK result with the category information if the retrieval is successful; otherwise, returns a BadRequest result with an error message if an exception occurs during the retrieval process.
        /// </returns>
        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id)
        {
            try
            {
                var category = categoryService.GetCategoryById(id);
                return Ok(category);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Gets a category by its name. The method takes a category name as a parameter and attempts to retrieve the corresponding category from the database using the category service. If the category is found, it returns the category information. If any exceptions occur during the retrieval process, a bad request response is returned with the error message.
        /// </summary>
        /// <remarks>
        /// The method validates the input category name and processes the get request accordingly. If the category name is valid and the category is successfully retrieved, it returns an OK response with the category information. If the category name is invalid or any exceptions occur during the retrieval process, it returns a BadRequest response with an appropriate error message.
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>
        /// An IActionResult indicating the result of the get operation. Returns an OK result with the category information if the retrieval is successful; otherwise, returns a BadRequest result with an error message if an exception occurs during the retrieval process.
        /// </returns>
        [HttpGet("{categoryName}")]
        public IActionResult GetCategoryByName(CategoriesCollections categoryName)
        {
            try
            {
                var category = categoryService.GetCategoryByName(categoryName.ToString());
                return Ok(category);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Adds a new category to the system based on the provided category data. The method takes a CreateCategoryDto object as input, which contains the necessary information for creating a new category. It attempts to add the category to the database using the category service. If the addition is successful, it returns a success message. If any exceptions occur during the addition process, a bad request response is returned with the error message.
        /// </summary>
        /// <remarks>
        /// The method validates the input category data and processes the add request accordingly. If the category data is valid and the category is successfully added, it returns an OK response with a success message. If the category data is invalid or any exceptions occur during the addition process, it returns a BadRequest response with an appropriate error message.
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>
        /// An IActionResult indicating the result of the add operation. Returns an OK result with a success message if the addition is successful; otherwise, returns a BadRequest result with an error message if an exception occurs during the addition process.
        /// </returns>
        [HttpPost]
        public IActionResult AddCategory([FromBody] CreateCategoryDto createCategoryDto)
        {
            try
            {
                categoryService.AddCategory(createCategoryDto);
                return Ok("This category created successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Updates an existing category in the system based on the provided category data. The method takes a CategoryDto object as input, which contains the updated information for the category. It attempts to update the category in the database using the category service. If the update is successful, it returns a success message. If any exceptions occur during the update process, a bad request response is returned with the error message.
        /// </summary>
        /// <remarks>
        /// The method validates the input category data and processes the update request accordingly. If the category data is valid and the category is successfully updated, it returns an OK response with a success message. If the category data is invalid or any exceptions occur during the update process, it returns a BadRequest response with an appropriate error message.
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>
        /// An IActionResult indicating the result of the update operation. Returns an OK result with a success message if the update is successful; otherwise, returns a BadRequest result with an error message if an exception occurs during the update process.
        /// </returns>
        [HttpPut]
        public IActionResult UpdateCategory([FromBody] CategoryDto categoryDto)
        {
            try
            {
                categoryService.UpdateCategory(categoryDto);
                return Ok("this category updated successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Deletes a category from the system based on the provided unique identifier (ID). The method takes an integer ID as a parameter and attempts to delete the corresponding category from the database. If the deletion is successful, it returns a success message. If any exceptions occur during the deletion process, a bad request response is returned with the error message.
        /// </summary>
        /// <remarks>
        /// The method validates the input ID and processes the delete request accordingly. If the ID is valid and the category is successfully deleted, it returns an OK response with a success message. If the ID is invalid or any exceptions occur during the deletion process, it returns a BadRequest response with an appropriate error message.
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>
        /// An IActionResult indicating the result of the delete operation. Returns an OK result with a success message if the deletion is successful; otherwise, returns a BadRequest result with an error message if an exception occurs during the deletion process.
        /// </returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            try
            {
                categoryService.DeleteCategory(id);
                return Ok("this category deleted successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
