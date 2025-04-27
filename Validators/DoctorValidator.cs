using FluentValidation;
using MediCore.DTOs.DoctorDTOs;
namespace MediCore.Validators;
public class DoctorValidator : AbstractValidator<DoctorUpdateDTO>
{
    public DoctorValidator()
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
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[0-9]").WithMessage("Password must contain at least one number.")
            .Length(4, 40).WithMessage("Password must be between 4 and 40 characters.");

        RuleFor(x => x.Specialty)
            .NotEmpty().WithMessage("Specialty is required.")
            .MaximumLength(50).WithMessage("Specialty cannot be longer than 50 characters.");

        RuleFor(x => x.LicenseNumber)
            .NotEmpty().WithMessage("License number is required.")
            .MaximumLength(20).WithMessage("License number cannot be longer than 20 characters.");

        RuleFor(x => x.WorkingHours)
            .MaximumLength(100).WithMessage("Working hours cannot be longer than 100 characters.");
    }
}
