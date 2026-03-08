namespace E_Commerce.API.Helpers
{
    /// <summary>
    /// This class represents a configuration model for JSON Web Tokens (JWT) in an e-commerce application. It contains properties to hold the secret key used for signing the JWT (SecretKey), the issuer of the JWT (Issuer), the intended audience of the JWT (Audience), and the duration in days for which the JWT is valid (DurationInDays). This class is typically used to store and manage JWT-related settings in the application, allowing for secure authentication and authorization mechanisms when generating and validating JWTs for users.
    /// </summary>
    public class JWT
    {
        public string SecretKey { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public double DurationInDays { get; set; }
    }
}
