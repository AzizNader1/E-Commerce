namespace E_Commerce.MVC.Services
{
    public interface IApiUserService
    {

    }
    public class ApiUserService : IApiUserService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ApiUserService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
    }
}