using E_Commerce.MVC.DTOs.CartItemDTOs;

namespace E_Commerce.MVC.DTOs.CartDTOs
{
    /// <summary>
    /// This class represents a data transfer object (DTO) for a shopping cart in an e-commerce application. 
    /// It contains properties to hold the cart's unique identifier (CartId), 
    /// the identifier of the user who owns the cart (UserId), 
    /// and a list of cart items (CartItems) that are associated with the cart.
    /// Each cart item is represented by a CartItemDto, which likely contains details about the product, 
    /// quantity, and price. This DTO is used to transfer cart data between different layers of the application,
    /// such as between the API and the service layer or between the service layer and the database.
    /// </summary>
    public class CartDto
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public List<CartItemDto> CartItems { get; set; }
    }
}
