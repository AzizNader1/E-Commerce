using System.Text.Json;
using System.Text.Json.Serialization;

namespace E_Commerce.MVC.Services
{
    public interface IApiProductService
    {
        Task<List<ProductDto>> GetAllProductsAsync();
        Task<ProductDto?> GetProductByIdAsync(int id);
    }
    public class ApiProductService : IApiProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ApiProductService(IHttpClientFactory httpClientFactory)
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
                return JsonSerializer.Deserialize<ProductDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return null;
        }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CategoriesCollections { }

    public class ProductDto
    {
        // Other properties...

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CategoriesCollections? CategoryName { get; set; }
    }
}
