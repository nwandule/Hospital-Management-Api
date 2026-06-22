using System.ComponentModel.DataAnnotations;

namespace Hospital_Management_system.Models.Domain
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string Role { get; set; } = "Patient";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Patient? Patient { get; set; }
        public Doctor? Doctor { get; set; }
    }
}