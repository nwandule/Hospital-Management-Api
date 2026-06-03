using Hospital_Management_system.Models.Domain;
using Hospital_Management_system.Services.CommonRepository;

namespace Hospital_Management_system.Services.PatientRepository
{
    public interface IPatientService: ICommonRepository<Patient>
    {
        
    }
}
