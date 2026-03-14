using E_Commerce.MVC.DTOs.OrderItemDTOs;
using E_Commerce.MVC.DTOs.UserDTOs;

namespace E_Commerce.MVC.DTOs.OrderDTOs
{
    public class OrderDetailsViewModel
    {
        public UserDto User { get; set; }
        public OrderDto Order { get; set; }
        public List<OrderWithProductsItemDto> OrderWithProductsItems { get; set; }
    }
}
