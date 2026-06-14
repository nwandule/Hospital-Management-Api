/*=============================================================================
 * Author:       Vikash
 * Description:  Core Domain Model representing a User entity. Defines the 
 * official structure and schema blueprint for User records as 
 * they are persisted dynamically within the SQL database.
 * Created Date: June 2026
 *=============================================================================*/
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
    }
}
