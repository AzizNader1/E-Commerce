using System.ComponentModel.DataAnnotations;

namespace E_Commerce.API.DTOs.UserDTOs
{
    /// <summary>
    /// This class represents a data transfer object (DTO) for updating an existing user in an e-commerce application. It contains properties to hold the user's ID (Id), username (UserName), email address (UserEmail), full name (UserFullName), address (UserAddress), and phone number (UserPhoneNumber). The Id property is marked as required, indicating that it must be provided when updating a user. The UserName, UserEmail, UserFullName, and UserAddress properties are also required and have maximum length constraints to ensure that the input data is valid. The UserEmail property is further validated to ensure that it is in a valid email format. The UserPhoneNumber property is optional and does not have any validation constraints. This DTO is used to transfer user data from the client to the server when updating an existing user in the system.
    /// </summary>
    public class UpdateUserDto
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "User Name Is Required Field And You Can Not Left It Empty")]
        [MaxLength(100, ErrorMessage = "Your User Name Must Not Exceed 100 Charater")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "User Email Is Required Field And You Can Not Left It Empty")]
        [MaxLength(100, ErrorMessage = "Your User Email Must Not Exceed 100 Charater")]
        [EmailAddress(ErrorMessage = "Please enter a vaild email address")]
        public string UserEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "FullName Is Required Field And You Can Not Left It Empty")]
        [MaxLength(100, ErrorMessage = "Your FullName Must Not Exceed 100 Charater")]
        public string UserFullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address Is Required Field And You Can Not Left It Empty")]
        [MaxLength(100, ErrorMessage = "Your Address Must Not Exceed 100 Charater")]
        public string UserAddress { get; set; } = string.Empty;

        public string UserPhoneNumber { get; set; } = string.Empty;

    }
}
