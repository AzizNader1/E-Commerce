using System.ComponentModel.DataAnnotations;

namespace E_Commerce.MVC.DTOs.UserDTOs
{
    /// <summary>
    /// This class represents a data transfer object (DTO) for user login in an e-commerce application. It contains properties to hold the user's username (UserName), email address (UserEmail), and password (UserPassword). The UserName and UserEmail properties are marked as required and have maximum length constraints to ensure that they do not exceed 100 characters. The UserEmail property also has an email address validation attribute to ensure that it is in a valid email format. The UserPassword property is required and must be at least 12 characters long, with a data type of password to ensure that it is treated securely. This DTO is used to transfer user login data from the client to the server when a user attempts to log in to the system.
    /// </summary>
    public class LoginUserDto
    {
        [Required(ErrorMessage = "User Name Is Required Field And You Can Not Left It Empty")]
        [MaxLength(100, ErrorMessage = "Your User Name Must Not Exceed 100 Charater")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email Is Required Field And You Can Not Left It Empty")]
        [MaxLength(100, ErrorMessage = "Your Email Must Not Exceed 100 Charater")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string UserEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password Is Required Field And You Can Not Left It Empty")]
        [MinLength(12, ErrorMessage = "Password must be at least 12 characters")]
        [DataType(DataType.Password, ErrorMessage = "Please Enter a strong password")]
        public string UserPassword { get; set; } = string.Empty;
    }
}