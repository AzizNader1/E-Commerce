using E_Commerce.MVC.DTOs.ProductDTOs;
using E_Commerce.MVC.DTOs.UserDTOs;

namespace E_Commerce.MVC.DTOs.OrderDTOs
{
    public class CheckoutFromCartViewModel
    {
        public UserDto User { get; set; }
        public List<ProductDto> Products { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
