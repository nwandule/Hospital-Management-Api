/*=============================================================================
 * Author:       Vikash
 * Description:  Input validation rules for public patient registrations.
 * Intercepts incoming requests to guarantee data integrity before processing.
 * Created Date: June 2026
 *=============================================================================*/
using FluentValidation;
using Hospital_Management_system.Models.Dto;

namespace Hospital_Management_system.Api.Validators
{
    public class RegisterPatientValidator : AbstractValidator<RegisterPatientDto>
    {
        public RegisterPatientValidator()
        {
            // 1. Email Rules
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email address is required.")
                .EmailAddress().WithMessage("Please enter a valid email address.");

            // 2. Password Rules
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");

            // 3. Profile Information Rules
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full name is required.")
                .MaximumLength(100).WithMessage("Full name cannot exceed 100 characters.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .MaximumLength(20).WithMessage("Phone number cannot exceed 20 characters.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Home address is required.")
                .MaximumLength(250).WithMessage("Address cannot exceed 250 characters.");
        }
    }
}