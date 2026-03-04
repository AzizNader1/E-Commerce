namespace E_Commerce.API.DTOs.UserDTOs
{
    public class UserDto
    {
        public int UserId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string UserEmail { get; set; } = string.Empty;

        public string UserPassword { get; set; } = string.Empty;

        public string? UserFullName { get; set; }

        public string? UserAddress { get; set; }

        public string UserPhoneNumber { get; set; } = string.Empty;
    }
}
