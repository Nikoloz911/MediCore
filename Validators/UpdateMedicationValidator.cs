using FluentValidation;
using MediCore.DTOs.MedicationsDTOs;

namespace MediCore.Validators;

public class UpdateMedicationValidator : AbstractValidator<UpdateMedicationDTO>
{
    public UpdateMedicationValidator()
    {
        RuleFor(m => m.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(2, 100);

        RuleFor(m => m.ActiveSubstance)
            .NotEmpty().WithMessage("Active substance is required.")
            .Length(2, 100);

        RuleFor(m => m.Category)
            .NotEmpty().WithMessage("Category is required.")
            .Length(2, 50);

        RuleFor(m => m.Dosage)
            .NotEmpty().WithMessage("Dosage is required.")
            .Length(2, 50);

        RuleFor(m => m.Form)
            .NotEmpty().WithMessage("Form is required.")
            .Length(2, 50);
    }
}
