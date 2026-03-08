namespace E_Commerce.MVC.Services
{
    public class ApiCartsService : IApiCartsService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ApiCartsService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

    }
}
