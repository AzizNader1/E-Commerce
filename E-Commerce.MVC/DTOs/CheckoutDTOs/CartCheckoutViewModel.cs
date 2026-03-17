using E_Commerce.MVC.DTOs.UserDTOs;

namespace E_Commerce.MVC.DTOs.CheckoutDTOs
{
    public class CartCheckoutViewModel
    {
        public UserDto? User { get; set; }
        public List<CartItemCheckoutDto> SelectedItems { get; set; } = new();

        public int TotalItems => SelectedItems?.Sum(item => item.Quantity) ?? 0;
        public decimal Subtotal => SelectedItems?.Sum(item => item.TotalPrice) ?? 0;
        public decimal Shipping => 0;
        public decimal Tax => 0;
        public decimal TotalPrice => Subtotal + Shipping + Tax;
    }
}

