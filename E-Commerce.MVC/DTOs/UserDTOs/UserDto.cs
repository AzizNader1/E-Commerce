namespace E_Commerce.MVC.DTOs.UserDTOs
{
    /// <summary>
    /// This class represents a data transfer object (DTO) for a user in an e-commerce application. It contains properties to hold the user's unique identifier (UserId), username (UserName), email address (UserEmail), password (UserPassword), full name (UserFullName), address (UserAddress), and phone number (UserPhoneNumber). The UserId is an integer that uniquely identifies the user, while the other properties are strings that store the user's information. This DTO is used to transfer user data between different layers of the application, such as from the database to the client or vice versa.
    /// </summary>
    public class UserDto
    {
        public int UserId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string UserEmail { get; set; } = string.Empty;

        public string UserPassword { get; set; } = string.Empty;

        public string? UserFullName { get; set; }

        public string? UserAddress { get; set; }

        public string UserPhoneNumber { get; set; } = string.Empty;
    }
}
