using System.ComponentModel.DataAnnotations;

namespace E_Commerce.API.DTOs.UserDTOs
{
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
