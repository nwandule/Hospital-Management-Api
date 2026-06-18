/*=============================================================================
 * Author:       Vikash
 * Description:  Data Transfer Object (DTO) for Doctor responses. Defines the 
 * clean, structured schema returned back to frontend clients, keeping the 
 * internal database structure safely decoupled.
 * Created Date: June 2026
 *=============================================================================*/
namespace Hospital_Management_system.Models.Dto
{
    public class DoctorResponseDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Specialization { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public bool IsAvailable { get; set; }
    }
}
