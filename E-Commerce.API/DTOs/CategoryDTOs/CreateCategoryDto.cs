using E_Commerce.API.Models;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.API.DTOs.CategoryDTOs
{
    /// <summary>
    /// This class represents a data transfer object (DTO) for creating a new category in an e-commerce application. It contains properties to hold the name of the category (CategoryName) and a description of the category (CategoryDescription). The CategoryName property is marked as required, meaning that it must be provided when creating a new category. The CategoryDescription property has a string length constraint, allowing it to be between 10 and 500 characters if provided. This DTO is used to transfer category data from the client to the server when creating a new category in the system.
    /// </summary>
    public class CreateCategoryDto
    {
        [Required(ErrorMessage = "Category Name Is Required Field And You Can Not Left It Empty")]
        public CategoriesCollections? CategoryName { get; set; }

        [StringLength(500, MinimumLength = 10,
            ErrorMessage = "Category Description Must Be Between 10 And 500 Characters If Provided")]
        public string CategoryDescription { get; set; } = string.Empty;
    }
}