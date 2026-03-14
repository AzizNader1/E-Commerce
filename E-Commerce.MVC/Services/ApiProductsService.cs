using E_Commerce.MVC.DTOs.ProductDTOs;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace E_Commerce.MVC.Services
{
    public class ApiProductsService : IApiProductsService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ApiProductsService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private static JsonSerializerOptions CreateJsonOptions()
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            options.Converters.Add(new JsonStringEnumConverter());
            return options;
        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            var client = _httpClientFactory.CreateClient("ECommerceApi");
            var response = await client.GetAsync("Products/GetAllProducts");
            if (!response.IsSuccessStatusCode)
                return new List<ProductDto>();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ProductDto>>(content, CreateJsonOptions()) ?? new List<ProductDto>();
        }

        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            var client = _httpClientFactory.CreateClient("ECommerceApi");
            var response = await client.GetAsync($"Products/GetProductById/{id}");
            if (!response.IsSuccessStatusCode)
                return null;

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ProductDto>(content, CreateJsonOptions());
        }

        public async Task<ProductDto?> CreateProductAsync(CreateProductDto dto, IFormFile? productImage = null)
        {
            var client = _httpClientFactory.CreateClient("ECommerceApi");

            using var content = new MultipartFormDataContent();

            // Add product data as JSON

            content.Add(new StringContent(dto.ProductName ?? string.Empty), "createProductDto.ProductName");
            content.Add(new StringContent(dto.ProductDescription ?? string.Empty), "createProductDto.ProductDescription");
            content.Add(new StringContent(dto.ProductPrice.ToString()), "createProductDto.ProductPrice");
            content.Add(new StringContent(dto.ProductStockQuantity.ToString()), "createProductDto.ProductStockQuantity");
            content.Add(new StringContent(dto.CategoryId.ToString()), "createProductDto.CategoryId");

            // Add image file if provided
            if (productImage != null && productImage.Length > 0)
            {
                using var memoryStream = new MemoryStream();
                await productImage.CopyToAsync(memoryStream);
                var fileBytes = memoryStream.ToArray();

                var fileContent = new ByteArrayContent(fileBytes);
                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(productImage.ContentType);
                content.Add(fileContent, "productImage", productImage.FileName);
            }

            var response = await client.PostAsync("Products/AddProduct", content);
            if (!response.IsSuccessStatusCode)
                return null;

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ProductDto>(responseContent, CreateJsonOptions());
        }

        public async Task<ProductDto?> UpdateProductAsync(UpdateProductDto dto, IFormFile? productImage = null)
        {
            var client = _httpClientFactory.CreateClient("ECommerceApi");

            using var content = new MultipartFormDataContent();

            // Add product data as JSON

            content.Add(new StringContent(dto.ProductName ?? string.Empty), "createProductDto.ProductName");
            content.Add(new StringContent(dto.ProductDescription ?? string.Empty), "createProductDto.ProductDescription");
            content.Add(new StringContent(dto.ProductPrice.ToString()), "createProductDto.ProductPrice");
            content.Add(new StringContent(dto.ProductStockQuantity.ToString()), "createProductDto.ProductStockQuantity");
            content.Add(new StringContent(dto.CategoryId.ToString()), "createProductDto.CategoryId");

            // Add image file if provided
            if (productImage != null && productImage.Length > 0)
            {
                using var memoryStream = new MemoryStream();
                await productImage.CopyToAsync(memoryStream);
                var fileBytes = memoryStream.ToArray();

                var fileContent = new ByteArrayContent(fileBytes);
                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(productImage.ContentType);
                content.Add(fileContent, "productImage", productImage.FileName);
            }

            var response = await client.PutAsync("Products/UpdateProduct", content);
            if (!response.IsSuccessStatusCode)
                return null;

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ProductDto>(responseContent, CreateJsonOptions());
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var client = _httpClientFactory.CreateClient("ECommerceApi");
            var response = await client.DeleteAsync($"Products/DeleteProduct/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
