namespace E_Commerce.MVC.Services
{
    public class ApiCategoriesService : IApiCategoriesService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ApiCategoriesService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

    }
}
