using E_Commerce.API.Repositories;
using E_Commerce.API.UnitOfWork;
using E_Commerce.API.Models;
using E_Commerce.API.DTOs.ProductDTOs;

namespace E_Commerce.API.Services
{
    public class ProductService : IProductService
    {
        private readonly UOW _uow;
        private const int MAX_SIZE_IN_BYTE = 5 * 1024 * 1024;
        private static readonly string[] AllowedImageTypes = { "image/jpeg", "image/jpg", "image/png", "image/webp" };

        public ProductService(UOW uow)
        {
            _uow = uow;
        }

        public ProductDto AddProductAsync(CreateProductDto createProductDto, IFormFile productImage)
        {
            if (createProductDto == null)
                throw new ArgumentNullException(nameof(createProductDto), "the product data can not be left empty");

            var selectedCategory = _uow.CategoryRepository.GetByIdAsync(createProductDto.CategoryId);
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
                Name = createProductDto.Name,
                Description = createProductDto.Description,
                Price = createProductDto.Price,
                StockQuantity = createProductDto.StockQuantity,
                CategoryId = createProductDto.CategoryId,
                ImageData = imageData,
                ImageContentType = imageContentType
            };

            _uow.ProductRepository.AddAsync(newProduct);
            
            return MapToDto(newProduct);
        }

        public void DeleteProductAsync(int productId)
        {
            if (productId == null || productId == 0)
                throw new ArgumentNullException(nameof(productId), "invalid product id");

            var selectedProduct = _uow.ProductRepository.GetByIdAsync(productId);
            if (selectedProduct == null)
                throw new ArgumentNullException(nameof(selectedProduct), "there is no products exists in the database for that id");

            _uow.ProductRepository.DeleteAsync(productId);
        }

        public List<ProductDto> GetAllProductsAsync()
        {
            var products = _uow.ProductRepository.GetAllAsync();
            if (products == null || products.Count == 0)
                throw new ArgumentNullException(nameof(products), "there is no products exists in the database");

            var productDtos = new List<ProductDto>();

            foreach (var product in products)
            {
                productDtos.Add(new ProductDto
                {
                    ProductId = product.ProductId,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    StockQuantity = product.StockQuantity,
                    CategoryId = product.CategoryId,
                    CategoryName = product.Category != null ? product.Category.Name : string.Empty,
                    ImageBase64 = product.ImageData != null ? Convert.ToBase64String(product.ImageData) : null,
                    ImageContentType = product.ImageContentType
                });
            }
            return productDtos;
        }

        public List<ProductDto> GetAllProductsByCategoryId(int categoryId)
        {
            if (categoryId == null || categoryId == 0)
                throw new ArgumentNullException(nameof(categoryId), "invalid id");

            var selectedCategory = _uow.CategoryRepository.GetByIdAsync(categoryId);
            if (selectedCategory == null)
                throw new ArgumentNullException(nameof(selectedCategory), "there no categories exists for that id");

            var products = _uow.ProductRepository.GetAllAsync();
            if (products == null || products.Count == 0)
                throw new ArgumentNullException(nameof(products), "there is no products exists in the database");

            var productDtos = new List<ProductDto>();

            foreach (var product in products)
            {
                if (product.CategoryId == categoryId)
                {
                    productDtos.Add(new ProductDto
                    {
                        ProductId = product.ProductId,
                        Name = product.Name,
                        Description = product.Description,
                        Price = product.Price,
                        StockQuantity = product.StockQuantity,
                        CategoryId = product.CategoryId,
                        CategoryName = product.Category != null ? product.Category.Name : string.Empty,
                        ImageBase64 = product.ImageData != null ? Convert.ToBase64String(product.ImageData) : null,
                        ImageContentType = product.ImageContentType
                    });
                }
            }
            return productDtos;
        }

        public List<ProductDto> GetAllProductsByCategoryName(string categoryName)
        {
            if (categoryName == null)
                throw new ArgumentNullException(nameof(categoryName), "invalid data entried");

            var allCategories = _uow.CategoryRepository.GetAllAsync();
            if (allCategories == null || allCategories.Count == 0)
                throw new ArgumentNullException(nameof(allCategories), "there is no categories exists in the database");

            var selectedCategory = allCategories.FirstOrDefault(c => c.Name == categoryName)!;
            if (selectedCategory == null)
                throw new ArgumentNullException(nameof(selectedCategory), "there is no category in the database for the name you sended");

            var products = _uow.ProductRepository.GetAllAsync();
            if (products == null || products.Count == 0)
                throw new ArgumentNullException(nameof(products), "there is no products exists in the database");

            var productDtos = new List<ProductDto>();

            foreach (var product in products)
            {
                if (product.CategoryId == selectedCategory.CategoryId)
                {
                    productDtos.Add(new ProductDto
                    {
                        ProductId = product.ProductId,
                        Name = product.Name,
                        Description = product.Description,
                        Price = product.Price,
                        StockQuantity = product.StockQuantity,
                        CategoryId = product.CategoryId,
                        CategoryName = product.Category != null ? product.Category.Name : string.Empty,
                        ImageBase64 = product.ImageData != null ? Convert.ToBase64String(product.ImageData) : null,
                        ImageContentType = product.ImageContentType
                    });
                }
            }
            return productDtos;
        }

        public ProductDto GetProductByIdAsync(int productId)
        {
            if (productId == null || productId == 0)
                throw new ArgumentNullException(nameof(productId), "invalid data entried");

            var product = _uow.ProductRepository.GetByIdAsync(productId);
            if (product == null)
                throw new ArgumentNullException(nameof(product), "there is no product in the database for the id you entried");

            return new ProductDto
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                CategoryId = product.CategoryId,
                CategoryName = product.Category != null ? product.Category.Name : string.Empty,
                ImageBase64 = product.ImageData != null ? Convert.ToBase64String(product.ImageData) : null,
                ImageContentType = product.ImageContentType
            };
        }

        public ProductDto UpdateProductAsync(UpdateProductDto updateProductDto, IFormFile productImage)
        {
            if (updateProductDto == null)
                throw new ArgumentNullException(nameof(updateProductDto), "the data of the product you  entried can not be lefted as empty");

            var selectedProduct = _uow.ProductRepository.GetByIdAsync(updateProductDto.ProductId);
            if (selectedProduct == null)
                throw new ArgumentNullException(nameof(selectedProduct), "there is no products exists in the database for the data you wanna update");

            var selectedCategory = _uow.CategoryRepository.GetByIdAsync(updateProductDto.CategoryId);
            if (selectedCategory == null)
                throw new ArgumentNullException(nameof(selectedCategory), "there is no category in the database for the id you entried");
             
            byte[]? imageData = null;
            string? imageContentType = null;

            if (productImage != null)
            {
                ValidateImage(productImage);
                (imageData, imageContentType) = ConvertImageToByteArray(productImage).Result;
            }

            var updatedProduct = new Product
            {
                Name = updateProductDto.Name,
                Description = updateProductDto.Description,
                Price = updateProductDto.Price,
                StockQuantity = updateProductDto.StockQuantity,
                CategoryId = updateProductDto.CategoryId,
                ImageData = imageData ?? selectedProduct.ImageData,
                ImageContentType = imageContentType ?? selectedProduct.ImageContentType
            };

           _uow.ProductRepository.UpdateAsync(updatedProduct);
           return MapToDto(updatedProduct);
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

        private ProductDto MapToDto(Product product)
        {
            return new ProductDto
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                CategoryId = product.CategoryId,
                CategoryName = product.Category != null ? product.Category.Name : string.Empty,
                ImageBase64 = product.ImageData != null ? Convert.ToBase64String(product.ImageData) : null,
                ImageContentType = product.ImageContentType
            };

        }
    }
}