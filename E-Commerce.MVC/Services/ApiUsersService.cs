using E_Commerce.MVC.DTOs.UserDTOs;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace E_Commerce.MVC.Services
{
    public class ApiUsersService : IApiUsersService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ApiUsersService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<UserDto> GetUserByNameAsync(string userName)
        {
            var client = _httpClientFactory.CreateClient("ECommerceApi");
            var response = await client.GetAsync("Users/GetAllUsers");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                options.Converters.Add(new JsonStringEnumConverter());

                var allUsers = JsonSerializer.Deserialize<List<UserDto>>(content, options) ?? new List<UserDto>();
                var wantedUser = allUsers.Where(a => a.UserName == userName).FirstOrDefault();
                return wantedUser!;
            }
            return new UserDto();

        }

        public async Task<LoginResponseDto> UpdateUserAsync(UserDto userDto)
        {
            var client = _httpClientFactory.CreateClient("ECommerceApi");
            var content = new StringContent(JsonSerializer.Serialize(userDto), System.Text.Encoding.UTF8, "application/json");
            var response = await client.PutAsync("Users/UpdateUser", content);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return new LoginResponseDto
                {
                    IsAuthenticated = true,
                    ErrorMessage = "Profile updated successfully.",
                    UserName = userDto.UserName
                };
            }
            return new LoginResponseDto { IsAuthenticated = false, ErrorMessage = "Profile update failed. API returned error." };
        }
    }
}