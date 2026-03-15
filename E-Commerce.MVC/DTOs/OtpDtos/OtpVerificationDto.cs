using System.ComponentModel.DataAnnotations;

namespace E_Commerce.MVC.DTOs.OtpDTOs
{
    public class OtpVerificationDto
    {
        [Required(ErrorMessage = "OTP is required")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "OTP must be exactly 6 digits")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "OTP must contain only digits")]
        public string OtpCode { get; set; } = string.Empty;

        // Store pending user data for registration/login completion
        public string? PendingUserName { get; set; }
        public string? PendingAction { get; set; } // "Login" or "Register"
    }
}
