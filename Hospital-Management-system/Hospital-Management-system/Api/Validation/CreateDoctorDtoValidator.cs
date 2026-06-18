/*=============================================================================
 * Author:       Vikash
 * Description:  FluentValidation rule engine configuration for CreateDoctorDto.
 * Intercepts incoming HTTP request data to enforce structural, length, 
 * and formatting rules before processing handles execution.
 * Created Date: June 2026
 *=============================================================================*/
using FluentValidation;
using Hospital_Management_system.Models.Dto;

namespace Hospital_Management_system.Api.Validation
{
    public class CreateDoctorDtoValidator: AbstractValidator<CreateDoctorDto>
    {
        public CreateDoctorDtoValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Doctor's full name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email address is required.")
                .EmailAddress().WithMessage("A valid email address is required.");

            RuleFor(x => x.Specialization)
                .NotEmpty().WithMessage("Medical specialization is required.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Invalid phone number format.");
        }
    }
}
