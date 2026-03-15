namespace E_Commerce.MVC.Helpers
{
    /// <summary>
    /// Helper class for generating and validating OTP (One-Time Password) codes
    /// </summary>
    public static class OtpHelper
    {
        private static readonly Random _random = new Random();

        /// <summary>
        /// Generates a random 6-digit OTP code
        /// </summary>
        /// <returns>A 6-digit OTP as string</returns>
        public static string GenerateOtp(int length = 6)
        {
            var otp = string.Empty;
            for (int i = 0; i < length; i++)
            {
                otp += _random.Next(0, 10).ToString();
            }
            return otp;
        }

        /// <summary>
        /// Generates an OTP and stores it in session with expiration
        /// </summary>
        /// <param name="session">The current HttpContext session</param>
        /// <param name="length">Length of the OTP (default 6)</param>
        /// <param name="expirationMinutes">Expiration time in minutes (default 5)</param>
        /// <returns>The generated OTP</returns>
        public static string GenerateAndStoreOtp(ISession session, int length = 6, int expirationMinutes = 1)
        {
            var otp = GenerateOtp(length);

            // Store OTP in session
            session.SetString("OTP_Code", otp);
            session.SetString("OTP_CreatedAt", DateTime.UtcNow.ToString("O"));
            session.SetString("OTP_ExpirationMinutes", expirationMinutes.ToString());

            return otp;
        }

        /// <summary>
        /// Validates the provided OTP against the stored session OTP
        /// </summary>
        /// <param name="session">The current HttpContext session</param>
        /// <param name="providedOtp">The OTP provided by the user</param>
        /// <returns>Tuple indicating if valid and error message if any</returns>
        public static (bool IsValid, string? ErrorMessage) ValidateOtp(ISession session, string providedOtp)
        {
            var storedOtp = session.GetString("OTP_Code");
            var createdAtStr = session.GetString("OTP_CreatedAt");
            var expirationStr = session.GetString("OTP_ExpirationMinutes");

            // Check if OTP exists
            if (string.IsNullOrEmpty(storedOtp))
            {
                return (false, "No OTP found. Please request a new one.");
            }

            // Check expiration
            if (!string.IsNullOrEmpty(createdAtStr) && !string.IsNullOrEmpty(expirationStr))
            {
                var createdAt = DateTime.Parse(createdAtStr);
                var expirationMinutes = int.Parse(expirationStr);

                if (DateTime.UtcNow > createdAt.AddMinutes(expirationMinutes))
                {
                    ClearOtp(session);
                    return (false, "OTP has expired. Please request a new one.");
                }
            }

            // Validate OTP
            if (storedOtp != providedOtp)
            {
                return (false, "Invalid OTP. Please try again.");
            }

            // Clear OTP after successful validation (one-time use)
            ClearOtp(session);

            return (true, null);
        }

        /// <summary>
        /// Clears the OTP from session
        /// </summary>
        /// <param name="session">The current HttpContext session</param>
        public static void ClearOtp(ISession session)
        {
            session.Remove("OTP_Code");
            session.Remove("OTP_CreatedAt");
            session.Remove("OTP_ExpirationMinutes");
        }

        /// <summary>
        /// Gets the remaining time for OTP expiration in seconds
        /// </summary>
        /// <param name="session">The current HttpContext session</param>
        /// <returns>Remaining seconds, or 0 if expired/not found</returns>
        public static int GetRemainingSeconds(ISession session)
        {
            var createdAtStr = session.GetString("OTP_CreatedAt");
            var expirationStr = session.GetString("OTP_ExpirationMinutes");

            if (string.IsNullOrEmpty(createdAtStr) || string.IsNullOrEmpty(expirationStr))
                return 0;

            var createdAt = DateTime.Parse(createdAtStr);
            var expirationMinutes = int.Parse(expirationStr);
            var expirationTime = createdAt.AddMinutes(expirationMinutes);

            var remaining = (expirationTime - DateTime.UtcNow).TotalMinutes;
            return remaining > 0 ? (int)remaining : 0;
        }
    }
}
