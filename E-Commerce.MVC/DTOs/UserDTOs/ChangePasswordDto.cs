using System.ComponentModel.DataAnnotations;

namespace E_Commerce.MVC.DTOs.UserDTOs
{
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
