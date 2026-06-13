/*=============================================================================
 * Author:       Vikash
 * Description:  Data Transfer Object (DTO) for structured patient data responses.
 * Safe-guards domain entities by exposing only necessary data fields to the UI layer.
 * Created Date: June 2026
 *=============================================================================*/

namespace Hospital_Management_system.Models.Dto
{
    public class PatientResponseDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}