namespace E_Commerce.API.Models
{
    public enum UserRoles
    {
        Customer,
        Admin
    }
    public class User
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public string? UserPassword { get; set; }
        public string? UserFullName { get; set; }
        public string? UserAddress { get; set; }
        public string? UserPhoneNumber { get; set; }
        public UserRoles UserRole { get; set; }


        // Navigation properties
        public virtual Cart? Cart { get; set; }
        public virtual ICollection<Order> Orders { get; set; } = [];
    }
}