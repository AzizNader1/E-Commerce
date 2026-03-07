namespace E_Commerce.MVC.Services
{
    public interface IApiOrderService
    {
    }
    public class ApiOrderService : IApiOrderService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ApiOrderService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
    }
}
