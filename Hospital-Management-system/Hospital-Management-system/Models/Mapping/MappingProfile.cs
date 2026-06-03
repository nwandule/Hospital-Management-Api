using AutoMapper;
using Hospital_Management_system.Models.Domain;
using Hospital_Management_system.Models.Dto;

namespace Hospital_Management_system.Models.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            
            CreateMap<CreatePatient, Patient>();

            CreateMap<Patient, PatientResponseDto>();
        }
    }
}
