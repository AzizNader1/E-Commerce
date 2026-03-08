using System.ComponentModel.DataAnnotations;

namespace E_Commerce.API.DTOs.UserDTOs
{
    /// <summary>
    /// This class represents a data transfer object (DTO) for registering a new user in an e-commerce application. It contains properties to hold the user's name (UserName), email address (UserEmail), password (UserPassword), full name (UserFullName), address (UserAddress), and phone number (UserPhoneNumber). The UserName, UserEmail, UserPassword, UserFullName, and UserAddress properties are marked as required, meaning that they must be provided when registering a new user. The UserEmail property also has an email address validation attribute to ensure that the provided email is in a valid format. The UserPassword property has a minimum length requirement of 12 characters to enhance security. This DTO is used to transfer user registration data from the client to the server when creating a new user account in the system.
    /// </summary>
    public class RegisterUserDto
    {
        [Required(ErrorMessage = "User Name Is Required Field And You Can Not Left It Empty")]
        [MaxLength(100, ErrorMessage = "Your User Name Must Not Exceed 100 Charater")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "User Email Is Required Field And You Can Not Left It Empty")]
        [MaxLength(100, ErrorMessage = "Your User Email Must Not Exceed 100 Charater")]
        [EmailAddress(ErrorMessage = "Please enter a valid email")]
        public string UserEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password Is Required Field And You Can Not Left It Empty")]
        [MinLength(12, ErrorMessage = "Password must be at least 12 characters")]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "FullName Is Required Field And You Can Not Left It Empty")]
        [MaxLength(100, ErrorMessage = "Your FullName Must Not Exceed 100 Charater")]
        public string UserFullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address Is Required Field And You Can Not Left It Empty")]
        [MaxLength(100, ErrorMessage = "Your Address Must Not Exceed 100 Charater")]
        public string UserAddress { get; set; } = string.Empty;

        public string UserPhoneNumber { get; set; } = string.Empty;
    }
}
