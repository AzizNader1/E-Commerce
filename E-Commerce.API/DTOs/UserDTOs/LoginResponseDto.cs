namespace E_Commerce.API.DTOs.UserDTOs
{
    public class LoginResponseDto
    {
        public string ErrorMessage { get; set; } = string.Empty;
        public bool IsAuthenticated { get; set; }
        public string UserName { get; set; } = string.Empty;
        public List<string> UserRoles { get; set; } = [];
        public string UserToken { get; set; } = string.Empty;
        public DateTime TokenExpiresOn { get; set; }
    }
}
