using FluentValidation;
using MediCore.DTOs.LabTests_ResultsDTOs;
namespace MediCore.Validators;
public class AddLabResultsValidator : AbstractValidator<AddLabResultDTO>
{
    public AddLabResultsValidator()
    {
        RuleFor(x => x.LabTestId)
            .NotEmpty().WithMessage("LabTestId is required.")
            .GreaterThan(0).WithMessage("LabTestId must be greater than 0.");
        RuleFor(x => x.PatientId)
            .NotEmpty().WithMessage("PatientId is required.")
            .GreaterThan(0).WithMessage("PatientId must be greater than 0.");
        RuleFor(x => x.Result)
            .NotEmpty().WithMessage("Result is required.")
            .MaximumLength(500).WithMessage("Result must not exceed 500 characters.");
        RuleFor(x => x.TestDate)
            .NotEmpty().WithMessage("TestDate is required.")
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now)).WithMessage("TestDate cannot be in the future.");
        RuleFor(x => x.PerformingLab)
            .NotEmpty().WithMessage("PerformingLab is required.")
            .MaximumLength(100).WithMessage("PerformingLab must not exceed 100 characters.");
    }
}