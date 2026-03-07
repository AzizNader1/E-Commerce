using System.ComponentModel.DataAnnotations;

namespace E_Commerce.MVC.DTOs.ProductDTOs
{
    public class UpdateProductDto
    {
        [Required]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product Name Is Required Field And You Can Not Left It Empty")]
        [StringLength(100, MinimumLength = 3,
            ErrorMessage = "Product Name Must Be Between 3 And 100 Characters")]
        public string ProductName { get; set; } = string.Empty;

        [StringLength(500, MinimumLength = 10,
            ErrorMessage = "Product Description Must Be Between 10 And 500 Characters If Provided")]
        public string ProductDescription { get; set; } = string.Empty;

        [Required(ErrorMessage = "Product Price Is Required Field And You Can Not Left It Empty")]
        [Range(0.01, 1000000,
            ErrorMessage = "Product Price Must Be Between 0.01 And 1000000")]
        public decimal ProductPrice { get; set; }

        [Required(ErrorMessage = "Product Stock Quantity Is Required Field And You Can Not Left It Empty")]
        [Range(0, 100000,
            ErrorMessage = "Product Stock Quantity Must Be Between 0 And 100000")]
        public int ProductStockQuantity { get; set; }

        [Required(ErrorMessage = "Category Id Is Required Field And You Can Not Left It Empty")]
        public int CategoryId { get; set; }

        public bool UpdateImage { get; set; } = false;
    }
}