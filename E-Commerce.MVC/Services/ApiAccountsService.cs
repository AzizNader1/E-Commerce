using System.Text;
using System.Text.Json;
using E_Commerce.MVC.DTOs.UserDTOs;

namespace E_Commerce.MVC.Services
{
    public interface IApiAccountsService
    {
        Task<LoginResponseDto> LoginAsync(LoginUserDto loginUserDto);
        Task<LoginResponseDto> RegisterAsync(RegisterUserDto registerUserDto);
    }

    public class ApiAccountsService : IApiAccountsService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ApiAccountsService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginUserDto loginUserDto)
        {
            var client = _httpClientFactory.CreateClient("ECommerceApi");
            var content = new StringContent(JsonSerializer.Serialize(loginUserDto), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/Accounts/Login", content);
            
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<LoginResponseDto>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new LoginResponseDto();
            }

            return new LoginResponseDto { IsAuthenticated = false, ErrorMessage = "Invalid login attempt. API returned error." };
        }

        public async Task<LoginResponseDto> RegisterAsync(RegisterUserDto registerUserDto)
        {
            var client = _httpClientFactory.CreateClient("ECommerceApi");
            var content = new StringContent(JsonSerializer.Serialize(registerUserDto), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/Accounts/Register", content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<LoginResponseDto>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new LoginResponseDto();
            }

            return new LoginResponseDto { IsAuthenticated = false, ErrorMessage = "Registration failed. API returned error." };
        }
    }
}
