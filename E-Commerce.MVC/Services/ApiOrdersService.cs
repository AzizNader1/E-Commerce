namespace E_Commerce.MVC.Services
{
    public class ApiOrdersService : IApiOrdersService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ApiOrdersService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

    }
}
