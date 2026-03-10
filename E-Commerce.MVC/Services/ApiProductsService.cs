using E_Commerce.MVC.DTOs.ProductDTOs;
using System.Net.Http.Json;
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

        public async Task<ProductDto?> CreateProductAsync(CreateProductDto dto)
        {
            var client = _httpClientFactory.CreateClient("ECommerceApi");
            var response = await client.PostAsJsonAsync("Products/AddProduct", dto);
            if (!response.IsSuccessStatusCode)
                return null;

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ProductDto>(content, CreateJsonOptions());
        }

        public async Task<ProductDto?> UpdateProductAsync(UpdateProductDto dto)
        {
            var client = _httpClientFactory.CreateClient("ECommerceApi");
            var response = await client.PutAsJsonAsync("Products/UpdateProduct", dto);
            if (!response.IsSuccessStatusCode)
                return null;

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ProductDto>(content, CreateJsonOptions());
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var client = _httpClientFactory.CreateClient("ECommerceApi");
            var response = await client.DeleteAsync($"Products/DeleteProduct/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
