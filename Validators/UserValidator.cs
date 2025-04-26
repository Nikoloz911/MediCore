using MediCore.Request;
using FluentValidation;
namespace MediCore.Validators;
public class UserValidator : AbstractValidator<AddUser>
{
    public UserValidator()
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
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[0-9]").WithMessage("Password must contain at least one number.")
            .Length(4, 40).WithMessage("Password must be between 4 and 40 characters.");
    }
}
