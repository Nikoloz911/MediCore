using FluentValidation;
using MediCore.DTOs.MedicationsDTOs;
namespace MediCore.Validators;
public class AddMedicationValidator : AbstractValidator<AddMedicationDTO>
{
    public AddMedicationValidator()
    {
        RuleFor(m => m.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(2, 100).WithMessage("Name must be between 2 and 100 characters.");
        RuleFor(m => m.ActiveSubstance)
            .NotEmpty().WithMessage("Active substance is required.")
            .Length(2, 100).WithMessage("Active substance must be between 2 and 100 characters.");
        RuleFor(m => m.Dosage)
            .NotEmpty().WithMessage("Dosage is required.")
            .Length(2, 50).WithMessage("Dosage must be between 2 and 50 characters.");
        RuleFor(m => m.Category)
            .NotEmpty().WithMessage("Category is required.")
            .Length(2, 50).WithMessage("Category must be between 2 and 50 characters.");
        RuleFor(m => m.Form)
            .NotEmpty().WithMessage("Form is required.")
            .Length(2, 50).WithMessage("Form must be between 2 and 50 characters.");
    }
}