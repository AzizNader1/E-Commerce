using E_Commerce.MVC.Models;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.MVC.DTOs.CategoryDTOs
{
    public class CreateCategoryDto
    {
        [Required(ErrorMessage = "Category Name Is Required Field And You Can Not Left It Empty")]
        public CategoriesCollections? CategoryName { get; set; }

        [StringLength(500, MinimumLength = 10,
            ErrorMessage = "Category Description Must Be Between 10 And 500 Characters If Provided")]
        public string CategoryDescription { get; set; } = string.Empty;
    }
}