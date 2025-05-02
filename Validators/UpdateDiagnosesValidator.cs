using FluentValidation;
using MediCore.DTOs.DiagnosesDTOs;
namespace MediCore.Validators;
public class UpdateDiagnosesValidator : AbstractValidator<UpdateDiagnosesDTO>
{
    public UpdateDiagnosesValidator()
    {
        RuleFor(x => x.ICD10Code)
             .NotEmpty()
             .WithMessage("ICD10 code is required.")
             .Length(3, 10)
             .WithMessage("ICD10 code must be between 3 and 10 characters.");
        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required.")
            .Length(5, 100)
            .WithMessage("Description must be between 5 and 100 characters.");
        RuleFor(x => x.AdditionalComments)
            .MaximumLength(200)
            .WithMessage("Additional comments cannot exceed 200 characters.");
    }
}