namespace E_Commerce.MVC.Services
{
    public interface IApiCategoryService
    {
    }
    public class ApiCategoryService : IApiCategoryService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ApiCategoryService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
    }
}
