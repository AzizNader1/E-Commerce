using E_Commerce.MVC.DTOs.CategoryDTOs;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace E_Commerce.MVC.Services
{
    public class ApiCategoriesService : IApiCategoriesService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ApiCategoriesService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private static JsonSerializerOptions CreateJsonOptions()
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            options.Converters.Add(new JsonStringEnumConverter());
            return options;
        }

        public async Task<List<CategoryDto>> GetAllCategoriesAsync()
        {
            var client = _httpClientFactory.CreateClient("ECommerceApi");
            var response = await client.GetAsync("Categories/GetAllCategories");
            if (!response.IsSuccessStatusCode)
                return new List<CategoryDto>();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<CategoryDto>>(content, CreateJsonOptions()) ?? new List<CategoryDto>();
        }

        public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
        {
            var client = _httpClientFactory.CreateClient("ECommerceApi");
            var response = await client.GetAsync($"Categories/GetCategoryById/{id}");
            if (!response.IsSuccessStatusCode)
                return null;

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<CategoryDto>(content, CreateJsonOptions());
        }

        public async Task<bool> CreateCategoryAsync(CreateCategoryDto dto)
        {
            var client = _httpClientFactory.CreateClient("ECommerceApi");
            var response = await client.PostAsJsonAsync("Categories/AddCategory", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateCategoryAsync(CategoryDto dto)
        {
            var client = _httpClientFactory.CreateClient("ECommerceApi");
            var response = await client.PutAsJsonAsync("Categories/UpdateCategory", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var client = _httpClientFactory.CreateClient("ECommerceApi");
            var response = await client.DeleteAsync($"Categories/DeleteCategory/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
