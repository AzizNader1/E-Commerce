using System.ComponentModel.DataAnnotations;

namespace E_Commerce.API.DTOs.UserDTOs
{
    public class CreateUserDto
    {
        [Required]
        [MaxLength(100)]
        public string Username { get; set; }

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Password { get; set; }

        [Required]
        [MaxLength(100)]
        public string? FullName { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Address { get; set; }

        [Required]
        [MaxLength(100)]
        public string PhoneNumber { get; set; }
    }
}
