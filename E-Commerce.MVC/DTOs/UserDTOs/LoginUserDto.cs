using System.ComponentModel.DataAnnotations;

namespace E_Commerce.MVC.DTOs.UserDTOs
{
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