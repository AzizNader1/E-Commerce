using E_Commerce.API.Repositories;
using E_Commerce.API.UnitOfWork;
using E_Commerce.API.Models;
using E_Commerce.API.DTOs.ProductDTOs;

namespace E_Commerce.API.Services
{
    public class ProductService : IProductService
    {
        private readonly UOW _uow;
        public ProductService(UOW uow)
        {
            _uow = uow;
        }

        public void AddProductAsync(CreateProductDto createProductDto)
        {
            if(createProductDto == null)
                throw new ArgumentNullException(nameof(createProductDto),"the product data can not be left empty");
            
            _uow.ProductRepository.AddAsync(new Product
            {
                Name = createProductDto.Name,
                Description = createProductDto.Description,
                Price = createProductDto.Price,
                CategoryId = createProductDto.CategoryId,
                StockQuantity = createProductDto.StockQuantity
            });
        }

        public void DeleteProductAsync(int productId)
        {
            if(productId == null || productId == 0)
                throw new ArgumentNullException(nameof(productId),"invalid product id");
           
            Product selectedProduct = _uow.ProductRepository.GetByIdAsync(productId);
            if(selectedProduct == null)
                throw new ArgumentNullException(nameof(selectedProduct),"there is no products exists in the database for that id");
            
            _uow.ProductRepository.DeleteAsync(productId);
        }

        public List<ProductDto> GetAllProductsAsync()
        {
            List<Product> products = _uow.ProductRepository.GetAllAsync();
            if(products == null || products.Count == 0)
                throw new ArgumentNullException(nameof(products),"there is no products exists in the database");
            
            List<ProductDto> productDtos = new List<ProductDto>();
            
            foreach(var product in products)
            {
                productDtos.Add(new ProductDto
                {
                    ProductId = product.ProductId,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    StockQuantity = product.StockQuantity,
                    CategoryId = product.CategoryId
                });
            }
            return productDtos;
        }

        public List<ProductDto> GetAllProductsByCategoryId(int categoryId)
        {
            if(categoryId == null || categoryId == 0)
                throw new ArgumentNullException(nameof(categoryId),"invalid id");

            Category selectedCategory = _uow.CategoryRepository.GetByIdAsync(categoryId);
            if(selectedCategory == null)
                throw new ArgumentNullException(nameof(selectedCategory),"there no categories exists for that id");
            
            List<Product> products = _uow.ProductRepository.GetAllAsync();
            if(products == null || products.Count == 0)
                throw new ArgumentNullException(nameof(products),"there is no products exists in the database");
            
            List<ProductDto> productDtos = new List<ProductDto>();  
            
            foreach(var product in products)
            {
                if(product.CategoryId == categoryId)
                {
                    productDtos.Add(new ProductDto
                    {
                        ProductId = product.ProductId,
                        Name = product.Name,
                        Description = product.Description,
                        Price = product.Price,
                        StockQuantity = product.StockQuantity,
                        CategoryId = product.CategoryId
                    });
                }
            }
            return productDtos;
        }

        public List<ProductDto> GetAllProductsByCategoryName(string categoryName)
        {
            if (categoryName == null)
                throw new ArgumentNullException(nameof(categoryName),"invalid data entried");
            
            List<Category> allCategories = _uow.CategoryRepository.GetAllAsync();
            if (allCategories == null || allCategories.Count == 0)
                throw new ArgumentNullException(nameof(allCategories), "there is no categories exists in the database");
            
            Category selectedCategory = allCategories.FirstOrDefault(c => c.Name == categoryName)!;
            if (selectedCategory == null)
                throw new ArgumentNullException(nameof(selectedCategory),"there is no category in the database for the name you sended");
            
            List<Product> products = _uow.ProductRepository.GetAllAsync();
            if (products == null || products.Count == 0)
                throw new ArgumentNullException(nameof(products), "there is no products exists in the database");
            
            List<ProductDto> productDtos = new List<ProductDto>();
            
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
                        CategoryId = product.CategoryId
                    });
                }
            }
            return productDtos;
        }

        public ProductDto GetProductByIdAsync(int productId)
        {
            if(productId == null || productId == 0)
                throw new ArgumentNullException(nameof(productId),"invalid data entried");
            
            Product product = _uow.ProductRepository.GetByIdAsync(productId);
            if(product == null)
                throw new ArgumentNullException(nameof(product), "there is no product in the database for the id you entried");
            
            return new ProductDto
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                CategoryId = product.CategoryId
            };
        }

        public void UpdateProductAsync(ProductDto productDto)
        {
            if (productDto == null)
                throw new ArgumentNullException(nameof(productDto), "the data of the product you  entried can not be lefted as empty");
            
            Product selectedProduct = _uow.ProductRepository.GetByIdAsync(productDto.ProductId);
            if (selectedProduct == null)
                throw new ArgumentNullException(nameof(selectedProduct), "there is no products exists in the database for the data you wanna update");
            
            _uow.ProductRepository.UpdateAsync(new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                StockQuantity = productDto.StockQuantity,
            });
        }
    }
}