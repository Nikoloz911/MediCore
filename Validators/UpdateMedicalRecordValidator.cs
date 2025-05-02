using FluentValidation;
using MediCore.DTOs.MedicalRecordsDTOs;

namespace MediCore.Validators
{
    public class UpdateMedicalRecordValidator : AbstractValidator<UpdateMedicalRecordDTO>
    {
        public UpdateMedicalRecordValidator()
        {
            RuleFor(m => m.Complaints)
                .NotEmpty()
                .WithMessage("Complaints are required.")
                .MaximumLength(500)
                .WithMessage("Complaints must not exceed 500 characters.");

            RuleFor(m => m.Symptoms)
                .NotEmpty()
                .WithMessage("Symptoms are required.")
                .MaximumLength(500)
                .WithMessage("Symptoms must not exceed 500 characters.");

            RuleFor(m => m.Measurements)
                .NotEmpty()
                .WithMessage("Measurements are required.")
                .MaximumLength(500)
                .WithMessage("Measurements must not exceed 500 characters.");

            RuleFor(m => m.DoctorId)
                .NotEmpty()
                .WithMessage("Doctor ID is required.");
        }
    }
}
