using System.ComponentModel.DataAnnotations;

namespace E_Commerce.API.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(100)]
        public string? FullName { get; set; }

        [StringLength(200)]
        public string? Address { get; set; }

        [StringLength(20)]
        [Required]
        public string PhoneNumber { get; set; }

        // Relationships
        public virtual Cart? Cart { get; set; } // One-to-One (optional for User)
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>(); // One-to-Many
    }
}
