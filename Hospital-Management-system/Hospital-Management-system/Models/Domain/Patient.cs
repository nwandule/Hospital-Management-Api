/*=============================================================================
 * Author:       Vikash
 * Description:  Core Domain Model representing a Patient entity. Defines the 
 * official structure and schema blueprint for patient records as 
 * they are persisted dynamically within the SQL database.
 * Created Date: June 2026
 *=============================================================================*/
namespace Hospital_Management_system.Models.Domain
{
    public class Patient
    {

        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
