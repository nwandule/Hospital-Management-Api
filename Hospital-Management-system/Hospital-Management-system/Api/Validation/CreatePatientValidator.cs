/*=============================================================================
 * Author:       Vikash
 * Description:  Inbound Data Validation Rules for incoming patient payloads.
 * Leverages FluentValidation to strictly enforce schema correctness, 
 * required fields, and format integrity prior to controller execution.
 * Created Date: June 2026
 *=============================================================================*/

using FluentValidation;
using Hospital_Management_system.Models.Dto;

namespace Hospital_Management_system.Api.Validators
{
    public class CreatePatientValidator : AbstractValidator<CreatePatient>
    {
        public CreatePatientValidator()
        {
          
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Patient name is required.")
                .MinimumLength(2).WithMessage("Patient name must be at least 2 characters long.")
                .MaximumLength(100).WithMessage("Patient name cannot exceed 100 characters.");

           
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email address is required.")
                .EmailAddress().WithMessage("A valid email address framework is required.")
                .MaximumLength(150).WithMessage("Email cannot exceed 150 characters.");

            
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("A valid international phone number format is required.") // Enforces standard E.164 phone formats
                .MaximumLength(20).WithMessage("Phone number cannot exceed 20 characters.");

          
            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Home address is required.")
                .MinimumLength(5).WithMessage("Please provide a more complete address layout.")
                .MaximumLength(250).WithMessage("Address cannot exceed 250 characters.");
        }
    }
}