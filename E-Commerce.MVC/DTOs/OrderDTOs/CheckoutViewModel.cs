using E_Commerce.MVC.DTOs.CartDTOs;
using E_Commerce.MVC.DTOs.UserDTOs;

namespace E_Commerce.MVC.DTOs.OrderDTOs
{
    public class CheckoutViewModel
    {
        public CartShoppingDto? Cart { get; set; }
        public UserDto? User { get; set; }
    }
}
