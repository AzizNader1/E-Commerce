using System.ComponentModel.DataAnnotations;

namespace E_Commerce.API.DTOs.UserDTOs
{
    public class UserDto
    {
        public int UserId { get; set; }

        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public string? FullName { get; set; }

        public string? Address { get; set; }

        public string PhoneNumber { get; set; } = string.Empty;
    }
}
