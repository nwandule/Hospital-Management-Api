using Hospital_Management_system.Database.Entities;
using Hospital_Management_system.Models.Domain;
using Hospital_Management_system.Models.Dto;
using Hospital_Management_system.Services.CommonRepository;

namespace Hospital_Management_system.Services.PatientRepository
{
    public class PatientService : CommonRepository<Patient>, IPatientService
    {
        // 🧠 mimic database (in-memory list for now)
        private readonly List<Patient> _patients = new();
        private int _idCounter = 1;

        public PatientService(ApplicationDbContext context) : base(context) {
            // 🧠 Seed default data (fake database records)
            _patients = new List<Patient>
        {
            new Patient
            {
                Id = _idCounter++,
                FullName = "John Doe",
                Email = "john@example.com",
                PhoneNumber = "0812345678",
                Address = "Johannesburg"
            },
            new Patient
            {
                Id = _idCounter++,
                FullName = "Sarah Mokoena",
                Email = "sarah@example.com",
                PhoneNumber = "0823456789",
                Address = "Pretoria"
            },
            new Patient
            {
                Id = _idCounter++,
                FullName = "Thabo Nkosi",
                Email = "thabo@example.com",
                PhoneNumber = "0834567890",
                Address = "Soweto"
            }
        };
        }
        public Patient CreatePatient(CreatePatient dto)
        {
            var patient = new Patient
            {
                Id = _idCounter++,
                FullName = dto.FullName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Address = dto.Address
            };

            _patients.Add(patient);

            return patient;
        }

        public List<Patient> GetAllPatients()
        {
            return _patients;
        }

        public Patient GetPatientById(int id)
        {
            return _patients.FirstOrDefault(x => x.Id == id);
        }
    }
}
