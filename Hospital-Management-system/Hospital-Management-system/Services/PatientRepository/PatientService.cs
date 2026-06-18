using Hospital_Management_system.Database.Entities;
using Hospital_Management_system.Models.Domain;
using Hospital_Management_system.Models.Dto;
using Hospital_Management_system.Services.CommonRepository;

namespace Hospital_Management_system.Services.PatientRepository
{
    public class PatientService : CommonRepository<Patient>, IPatientService
    {
        
        public PatientService(ApplicationDbContext context) : base(context)
        {

        }
    }
}
