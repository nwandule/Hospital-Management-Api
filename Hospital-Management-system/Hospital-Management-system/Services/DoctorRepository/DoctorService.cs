using Hospital_Management_system.Database.Entities;
using Hospital_Management_system.Models.Domain;
using Hospital_Management_system.Services.CommonRepository;
using Hospital_Management_system.Services.PatientRepository;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management_system.Services.DoctorRepository
{
    public class DoctorService: CommonRepository<Doctor>, IDoctorService
    {
        public DoctorService(ApplicationDbContext context) : base(context)
        { 
        }
    }
}
