using System.ComponentModel.DataAnnotations;

namespace E_Commerce.MVC.DTOs.ProductDTOs
{
    /// <summary>
    /// This class represents a data transfer object (DTO) for creating a new product in an e-commerce application. It contains properties to hold the name of the product (ProductName), a description of the product (ProductDescription), the price of the product (ProductPrice), the stock quantity of the product (ProductStockQuantity), and the category ID to which the product belongs (CategoryId). The ProductName, ProductPrice, ProductStockQuantity, and CategoryId properties are marked as required, meaning that they must be provided when creating a new product. The ProductDescription property has a string length constraint, allowing it to be between 10 and 500 characters if provided. This DTO is used to transfer product data from the client to the server when creating a new product in the system.
    /// </summary>
    public class CreateProductDto
    {
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
        [Range(1, int.MaxValue,
            ErrorMessage = "Category Id Must Be A Valid Positive Number")]
        public int CategoryId { get; set; }
    }
}