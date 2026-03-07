namespace E_Commerce.MVC.Services
{
    public interface IApiAccountsService
    {
    }

    public class ApiAccountsService : IApiAccountsService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ApiAccountsService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
    }
}
