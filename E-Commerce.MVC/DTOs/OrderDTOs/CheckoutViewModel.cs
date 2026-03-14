using E_Commerce.MVC.DTOs.ProductDTOs;
using E_Commerce.MVC.DTOs.UserDTOs;

namespace E_Commerce.MVC.DTOs.OrderDTOs
{
    public class CheckoutViewModel
    {
        public ProductDto? Product { get; set; }
        public UserDto? User { get; set; }
        public decimal TotalPrice { get; set; }
        public int OrderQuantity { get; set; }
    }
}
