/*=============================================================================
 * Author:       Vikash
 * Description:  Core Domain Model representing a Doctor entity. Defines the 
 * official structure and schema blueprint for doctor records as 
 * they are persisted dynamically within the SQL database.
 * Created Date: June 2026
 *=============================================================================*/
using System.ComponentModel.DataAnnotations;

namespace Hospital_Management_system.Models.Domain
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Specialization { get; set; } = null!;

        [Required]
        public string PhoneNumber { get; set; } = null!;

        public bool IsAvailable { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
