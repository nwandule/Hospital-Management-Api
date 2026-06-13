/*=============================================================================
 * Author:       Vikash
 * Description:  Inbound Data Transfer Object (DTO) for patient registration. 
 * Serves as a secure data contract to capture raw user input from 
 * client applications prior to mapping into a domain entity.
 * Created Date: June 2026
 *=============================================================================*/
namespace Hospital_Management_system.Models.Dto
{
    public class CreatePatient
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
