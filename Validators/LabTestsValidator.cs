using FluentValidation;
using MediCore.DTOs.LabTests_ResultsDTOs;
namespace MediCore.Validators;
public class LabTestsValidator : AbstractValidator<AddLabTestsDTO>
{
    public LabTestsValidator()
    {
        RuleFor(x => x.TestType)
            .NotEmpty().WithMessage("Test Type is required.")
            .Length(1, 100).WithMessage("Test Type must be between 1 and 100 characters.");
        RuleFor(x => x.NormalRange)
            .NotEmpty().WithMessage("Normal Range is required.")
            .Length(1, 50).WithMessage("Normal Range must be between 1 and 50 characters.");
        RuleFor(x => x.Unit)
            .NotEmpty().WithMessage("Unit is required.")
            .Length(1, 20).WithMessage("Unit must be between 1 and 20 characters.");
    }
}