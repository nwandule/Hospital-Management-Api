/*=============================================================================
 * Author:       Vikash
 * Description:  Data Transfer Object (DTO) for creating a Doctor. Captures the 
 * required incoming request payload from the frontend client before 
 * validation and domain mapping processing.
 * Created Date: June 2026
 *=============================================================================*/
namespace Hospital_Management_system.Models.Dto
{
    public class CreateDoctorDto
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Specialization { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
    }
}
