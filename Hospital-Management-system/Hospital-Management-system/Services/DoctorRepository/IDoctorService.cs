/*=============================================================================
 * Author:       Vikash
 * Description:  Interface defining the core domain contract for doctor data operations.
 * Decouples the controller layer from underlying database persistence logic.
 * Created Date: June 2026
 *=============================================================================*/
using Hospital_Management_system.Models.Domain;
using Hospital_Management_system.Services.CommonRepository;

namespace Hospital_Management_system.Services.DoctorRepository
{
    public interface IDoctorService : ICommonRepository<Doctor>
    {
    }
}
