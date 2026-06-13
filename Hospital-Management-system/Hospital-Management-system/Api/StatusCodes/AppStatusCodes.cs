/*=============================================================================
 * Author:       Vikash
 * Description:  Centralized Application Status Code Registry. Defines static, 
 * unchanging internal error tracking constants (e.g., ERR_PATIENT_001) 
 * used to provide consistent error identities across the API, 
 * backend layers, and frontend client applications.
 * Created Date: June 2026
 *=============================================================================*/
namespace Hospital_Management_system.Api.StatusCodes
{
    public class AppStatusCodes
    {
        public const string PatientNotFound = "ERR_PATIENT_001";
        public const string DuplicateRecord = "ERR_PATIENT_002";
    }
}
