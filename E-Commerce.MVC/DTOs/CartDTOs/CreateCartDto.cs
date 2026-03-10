namespace E_Commerce.MVC.DTOs.CartDTOs
{
    /// <summary>
    /// This class represents a data transfer object (DTO) for creating a shopping cart in an e-commerce application. It contains a single property, UserId, which is an integer representing the identifier of the user for whom the cart is being created. This DTO is used to transfer the necessary information to create a new cart associated with a specific user in the system.
    /// </summary>
    public class CreateCartDto
    {
        public int UserId { get; set; }
    }
}
