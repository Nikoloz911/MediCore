using FluentValidation;
using MediCore.DTOs.PatientDTOs;

namespace MediCore.Validators;

public class PatientValidator : AbstractValidator<PatientAddDTO>
{
    public PatientValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First Name is required.")
            .Length(1, 40).WithMessage("First Name must be between 1 and 40 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last Name is required.")
            .Length(1, 60).WithMessage("Last Name must be between 1 and 60 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email is not valid.")
            .Must(email => email.Contains("@") && email.Trim().EndsWith(".com", StringComparison.OrdinalIgnoreCase))
            .WithMessage("Email must contain '@' and end with '.com'.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .Length(4, 40).WithMessage("Password must be between 4 and 40 characters.")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[0-9]").WithMessage("Password must contain at least one number.");

        RuleFor(x => x.PersonalNumber)
            .NotEmpty().WithMessage("Personal Number is required.")
            .Matches(@"^\d{11}$").WithMessage("Personal Number must be exactly 11 digits.");

        RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("Date of Birth is required.")
            .Must(date => date.Date <= DateTime.Now.Date).WithMessage("Date of Birth cannot be in the future.")
            .Must(date => date.Date == date).WithMessage("Date of Birth must not contain time.");

        RuleFor(x => x.Gender)
            .NotEmpty().WithMessage("Gender is required.")
            .MaximumLength(20).WithMessage("Gender must not exceed 20 characters.");

        RuleFor(x => x.ContactInfo)
            .NotEmpty().WithMessage("Contact Info is required.")
            .Length(1, 100).WithMessage("Contact Info must be between 1 and 100 characters.");

        RuleFor(x => x.InsuranceDetails)
            .NotEmpty().WithMessage("Insurance Details are required.")
            .MaximumLength(100).WithMessage("Insurance Details must not exceed 100 characters.");

        RuleFor(x => x.Allergies)
            .NotEmpty().WithMessage("Allergies information is required.")
            .MaximumLength(100).WithMessage("Allergies must not exceed 100 characters.");

        RuleFor(x => x.BloodType)
           .NotEmpty().WithMessage("Blood Type is required.")
           .MaximumLength(20).WithMessage("Blood Type must not exceed 20 characters.");
    }
}
