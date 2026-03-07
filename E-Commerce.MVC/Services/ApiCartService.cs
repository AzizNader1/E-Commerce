namespace E_Commerce.MVC.Services
{
    public interface IApiCartService
    {
    }
    public class ApiCartService : IApiCartService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ApiCartService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
    }
}
