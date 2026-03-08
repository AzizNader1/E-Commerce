namespace E_Commerce.MVC.Services
{
    public class ApiUsersService : IApiUsersService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ApiUsersService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

    }
}
