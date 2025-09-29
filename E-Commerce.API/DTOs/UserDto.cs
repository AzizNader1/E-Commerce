using System.ComponentModel.DataAnnotations;

namespace E_Commerce.API.DTOs
{
    public class UserDto
    {
        public int UserId { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string? FullName { get; set; }

        public string? Address { get; set; }

        public string PhoneNumber { get; set; }
    }
}
