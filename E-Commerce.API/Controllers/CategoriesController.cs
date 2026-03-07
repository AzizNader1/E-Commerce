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
