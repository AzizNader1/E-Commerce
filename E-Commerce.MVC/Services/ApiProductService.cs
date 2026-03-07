using E_Commerce.MVC.Models;
using System.Text.Json;

namespace E_Commerce.MVC.Services
{
    public interface IApiProductService
    {
        Task<List<Product>> GetAllProductsAsync();
    }
    public class ApiProductService : IApiProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ApiProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            var client = _httpClientFactory.CreateClient("ECommerceApi");
            var response = await client.GetAsync("/Products/GetAllProducts");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Product>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<Product>();
            }
            return new List<Product>();
        }
    }
}
