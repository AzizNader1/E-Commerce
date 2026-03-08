using System.ComponentModel.DataAnnotations;

namespace E_Commerce.API.DTOs.UserDTOs
{
    /// <summary>
    /// This class represents a data transfer object (DTO) for changing a user's password in an e-commerce application. It contains properties to hold the user ID (UserId), the current password (CurrentPassword), the new password (NewPassword), and a confirmation of the new password (ConfirmNewPassword). The UserId property is marked as required and must be a valid positive number. The CurrentPassword, NewPassword, and ConfirmNewPassword properties are also required and have validation attributes to ensure that they meet certain criteria, such as minimum length and matching passwords. This DTO is used to transfer password change data from the client to the server when a user wants to update their password in the system.
    /// </summary>
    public class ChangePasswordDto
    {
        [Required]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Current Password Is Required Field And You Can Not Left It Empty")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "New Password Is Required Field And You Can Not Left It Empty")]
        [MinLength(12, ErrorMessage = "Password must be at least 12 characters")]
        [DataType(DataType.Password, ErrorMessage = "Please enter a strong password")]
        public string NewPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirm New Password Is Required Field And You Can Not Left It Empty")]
        [Compare(nameof(NewPassword), ErrorMessage = "The new passwords do not match")]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; } = string.Empty;
    }
}
