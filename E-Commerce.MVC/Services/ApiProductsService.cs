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
        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            var client = _httpClientFactory.CreateClient("ECommerceApi");
            var response = await client.GetAsync("/api/Products/GetAllProducts");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                options.Converters.Add(new JsonStringEnumConverter());

                return JsonSerializer.Deserialize<List<ProductDto>>(content, options) ?? new List<ProductDto>();
            }
            return new List<ProductDto>();
        }

        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            var client = _httpClientFactory.CreateClient("ECommerceApi");
            var response = await client.GetAsync($"/api/Products/GetProductById/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<ProductDto>(content, options) ?? new ProductDto();
            }
            return null;
        }

    }
}
