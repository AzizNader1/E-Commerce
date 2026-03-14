using E_Commerce.API.DTOs.ProductDTOs;
using E_Commerce.API.Models;
using E_Commerce.API.UnitOfWork;

namespace E_Commerce.API.Services
{
    /// <summary>
    /// This class represents the service layer for managing products in an e-commerce application. It implements the IProductService interface and provides methods for adding, deleting, retrieving, and updating products. The service interacts with the database through a Unit of Work (UOW) pattern to perform operations on product data. Each method includes validation checks to ensure that the input data is valid and that the necessary related entities (such as categories) exist in the database. The service is responsible for handling all business logic related to products, ensuring that the application functions correctly when users create or modify product information, including handling product images with specific validation for image type and size.
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly UOW _uow;
        private const int MAX_SIZE_IN_BYTE = 5 * 1024 * 1024;
        private static readonly string[] AllowedImageTypes = { "image/jpeg", "image/jpg", "image/png", "image/webp" };

        public ProductService(UOW uow)
        {
            _uow = uow;
        }

        public ProductDto AddProduct(CreateProductDto createProductDto, IFormFile productImage)
        {
            if (createProductDto == null)
                throw new ArgumentNullException(nameof(createProductDto), "the product data can not be left empty");

            var selectedCategory = _uow.CategoryRepository.GetModelById(createProductDto.CategoryId);
            if (selectedCategory == null)
                throw new ArgumentNullException(nameof(selectedCategory), "there is no category in the database for the id you entried");

            byte[]? imageData = null;
            string? imageContentType = null;

            if (productImage != null)
            {
                ValidateImage(productImage);
                (imageData, imageContentType) = ConvertImageToByteArray(productImage).Result;
            }

            var newProduct = new Product
            {
                ProductName = createProductDto.ProductName,
                ProductDescription = createProductDto.ProductDescription,
                ProductPrice = createProductDto.ProductPrice,
                ProductStockQuantity = createProductDto.ProductStockQuantity,
                CategoryId = createProductDto.CategoryId,
                ProductImageData = imageData,
                ProductImageContentType = imageContentType
            };

            _uow.ProductRepository.AddModel(newProduct);

            return MapModelToDto(newProduct);
        }

        public void DeleteProduct(int productId)
        {
            if (productId <= 0)
                throw new ArgumentNullException(nameof(productId), "invalid product id");

            var selectedProduct = _uow.ProductRepository.GetModelById(productId);
            if (selectedProduct == null)
                throw new ArgumentNullException(nameof(selectedProduct), "there is no products exists in the database for that id");

            _uow.ProductRepository.DeleteModel(productId);
        }

        public List<ProductDto> GetAllProducts()
        {
            var products = _uow.ProductRepository.GetAllModels();
            if (products == null || products.Count == 0)
                throw new ArgumentNullException(nameof(products), "there is no products exists in the database");

            var productDtos = new List<ProductDto>();

            foreach (var product in products)
            {
                var category = _uow.CategoryRepository.GetModelById(product.CategoryId);
                product.Category!.CategoryName = category.CategoryName;
                productDtos.Add(MapModelToDto(product));
            }
            return productDtos;
        }

        public List<ProductDto> GetAllProductsByCategoryId(int categoryId)
        {
            if (categoryId <= 0)
                throw new ArgumentNullException(nameof(categoryId), "invalid category id");

            var selectedCategory = _uow.CategoryRepository.GetModelById(categoryId);
            if (selectedCategory == null)
                throw new ArgumentNullException(nameof(selectedCategory), "there no categories exists for that id");

            var products = _uow.ProductRepository.GetAllModels();
            if (products == null || products.Count == 0)
                throw new ArgumentNullException(nameof(products), "there is no products exists in the database");

            var productsByCategoryId = products.Where(p => p.CategoryId == categoryId).ToList();
            if (productsByCategoryId == null || productsByCategoryId.Count == 0)
                throw new ArgumentNullException(nameof(productsByCategoryId), "there is no products for this category");

            var productDtos = new List<ProductDto>();

            foreach (var product in productsByCategoryId)
            {
                if (product.CategoryId == categoryId)
                {
                    product.Category!.CategoryName = selectedCategory.CategoryName;
                    productDtos.Add(MapModelToDto(product));
                }
            }
            return productDtos;
        }

        public List<ProductDto> GetAllProductsByCategoryName(string categoryName)
        {
            if (categoryName == "" || categoryName == default)
                throw new ArgumentNullException(nameof(categoryName), "invalid category name");

            var categories = _uow.CategoryRepository.GetAllModels();
            var selectedCategory = categories.FirstOrDefault(c => c.CategoryName.ToString() == categoryName);
            if (selectedCategory == null)
                throw new ArgumentNullException(nameof(selectedCategory), "there no categories exists for this name");

            var products = _uow.ProductRepository.GetAllModels();
            if (products == null || products.Count == 0)
                throw new ArgumentNullException(nameof(products), "there is no products exists in the database");

            var productsByCategoryName = products.Where(p => p.Category!.CategoryName.ToString() == categoryName).ToList();
            if (productsByCategoryName == null || productsByCategoryName.Count == 0)
                throw new ArgumentNullException(nameof(productsByCategoryName), "there is no products for this category");

            var productDtos = new List<ProductDto>();

            foreach (var product in productsByCategoryName)
            {
                if (product.CategoryId == selectedCategory.CategoryId)
                {
                    product.Category!.CategoryName = selectedCategory.CategoryName;
                    productDtos.Add(MapModelToDto(product));
                }
            }
            return productDtos;
        }

        public ProductDto GetProductById(int productId)
        {
            if (productId <= 0)
                throw new ArgumentNullException(nameof(productId), "invalid data entried");

            var product = _uow.ProductRepository.GetModelById(productId);
            if (product == null)
                throw new ArgumentNullException(nameof(product), "there is no product in the database for the id you entried");

            var category = _uow.CategoryRepository.GetModelById(product.CategoryId);
            product.Category!.CategoryName = category.CategoryName;
            return MapModelToDto(product);
        }

        public ProductDto UpdateProduct(UpdateProductDto updateProductDto, IFormFile productImage)
        {
            if (updateProductDto == null)
                throw new ArgumentNullException(nameof(updateProductDto), "the data of the product you entried can not be lefted empty");

            var selectedProduct = _uow.ProductRepository.GetModelById(updateProductDto.ProductId);
            if (selectedProduct == null)
                throw new ArgumentNullException(nameof(selectedProduct), "there is no data exists in the database for the product you need to update");

            var selectedCategory = _uow.CategoryRepository.GetModelById(updateProductDto.CategoryId);
            if (selectedCategory == null)
                throw new ArgumentNullException(nameof(selectedCategory), "there is no category in the database for the id you entried");

            byte[]? imageData = null;
            string? imageContentType = null;
            var updatedProduct = new Product();

            if (productImage != null) // he need to update the image of the product because he upload a new image here
            {
                ValidateImage(productImage);
                (imageData, imageContentType) = ConvertImageToByteArray(productImage).Result;

                updatedProduct.ProductId = updateProductDto.ProductId;
                updatedProduct.ProductName = updateProductDto.ProductName;
                updatedProduct.ProductDescription = updateProductDto.ProductDescription;
                updatedProduct.ProductPrice = updateProductDto.ProductPrice;
                updatedProduct.ProductStockQuantity = updateProductDto.ProductStockQuantity;
                updatedProduct.CategoryId = updateProductDto.CategoryId;
                updatedProduct.ProductImageData = imageData ?? selectedProduct.ProductImageData;
                updatedProduct.ProductImageContentType = imageContentType ?? selectedProduct.ProductImageContentType;

                _uow.ProductRepository.UpdateModel(updatedProduct);
                return MapModelToDto(updatedProduct);
            }

            selectedProduct.ProductPrice = updateProductDto.ProductPrice;
            selectedProduct.ProductName = updateProductDto.ProductName;
            selectedProduct.ProductStockQuantity = updateProductDto.ProductStockQuantity;
            selectedProduct.ProductDescription = updateProductDto.ProductDescription;
            selectedProduct.CategoryId = updateProductDto.CategoryId;

            _uow.ProductRepository.UpdateModel(selectedProduct);
            return MapModelToDto(selectedProduct);

        }

        public ProductDto UpdateProductQuantity(int productId, int quantity)
        {
            var existingProduct = _uow.ProductRepository.GetModelById(productId);
            if (existingProduct == null)
                throw new ArgumentNullException(nameof(productId), "there is no avaliable product for this id");

            if (quantity > existingProduct.ProductStockQuantity)
                throw new ArgumentNullException(nameof(quantity), "the quantity that you asked for are not avaliable in the stock at this time");

            existingProduct.ProductStockQuantity -= quantity;

            _uow.ProductRepository.UpdateModel(existingProduct);
            return MapModelToDto(existingProduct);

        }
        private void ValidateImage(IFormFile productImage)
        {
            if (productImage == null)
                throw new ArgumentNullException(nameof(productImage), "the image can not be left empty");

            if (!AllowedImageTypes.Contains(productImage.ContentType))
                throw new ArgumentException("invalid image type. Allowed types are: " + string.Join(", ", AllowedImageTypes));

            if (productImage.Length > MAX_SIZE_IN_BYTE)
                throw new ArgumentException("the image size can not exceed 5MB");
        }

        private async Task<(byte[] imageData, string imageContentType)> ConvertImageToByteArray(IFormFile productImage)
        {
            using var memoryStream = new MemoryStream();
            await productImage.CopyToAsync(memoryStream);
            return (memoryStream.ToArray(), productImage.ContentType);
        }

        private ProductDto MapModelToDto(Product product)
        {
            return new ProductDto
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName!,
                ProductDescription = product.ProductDescription!,
                ProductPrice = product.ProductPrice,
                ProductStockQuantity = product.ProductStockQuantity,
                CategoryId = product.CategoryId,
                CategoryName = product.Category?.CategoryName,
                ProductImageContentType = product.ProductImageContentType,
                IsProductHasImage = product.ProductImageData == null ? false : true,
                ProductImage = product.ProductImageData
            };
        }
    }
}