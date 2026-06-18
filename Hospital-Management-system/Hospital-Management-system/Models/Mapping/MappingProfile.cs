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
            CreateMap<CreateDoctorDto, Doctor>();
            CreateMap<Doctor, DoctorResponseDto>();

            CreateMap<RegisterPatientDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => "Patient"));
        }
    }
}
