using System.ComponentModel.DataAnnotations;

namespace E_Commerce.MVC.DTOs.ProductDTOs
{
    /// <summary>
    /// This class represents a data transfer object (DTO) for updating an existing product in an e-commerce application. It contains properties to hold the product ID (ProductId), the name of the product (ProductName), a description of the product (ProductDescription), the price of the product (ProductPrice), the stock quantity of the product (ProductStockQuantity), and the category ID to which the product belongs (CategoryId). The ProductId, ProductName, ProductPrice, ProductStockQuantity, and CategoryId properties are marked as required, meaning that they must be provided when updating a product. The ProductDescription property has a string length constraint, allowing it to be between 10 and 500 characters if provided. Additionally, there is a boolean property called UpdateImage that indicates whether the product image should be updated or not. This DTO is used to transfer product data from the client to the server when updating an existing product in the system.
    /// </summary>
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