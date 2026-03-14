namespace E_Commerce.API.DTOs.UserDTOs
{
    /// <summary>
    /// This class represents a data transfer object (DTO) for the response of a user login operation in an e-commerce application. It contains properties to hold the error message (if any), a boolean indicating whether the authentication was successful, the username of the authenticated user, a list of roles assigned to the user, a token for authentication purposes, and the expiration date and time of the token. This DTO is used to transfer login response data from the server to the client after a login attempt is made.
    /// </summary>
    public class LoginResponseDto
    {
        public int UserId { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public bool IsAuthenticated { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserRoles { get; set; } = string.Empty;
        public string UserToken { get; set; } = string.Empty;
        public DateTime TokenExpiresOn { get; set; }
    }
}
