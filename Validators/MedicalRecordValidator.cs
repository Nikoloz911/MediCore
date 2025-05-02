using FluentValidation;
using MediCore.DTOs.MedicalRecordsDTOs;

namespace MediCore.Validators;

public class MedicalRecordValidator : AbstractValidator<CreateMedicalRecordDTO>
{
    public MedicalRecordValidator()
    {
        RuleFor(m => m.Complaints)
            .NotEmpty().WithMessage("Complaints are required.")
            .MaximumLength(500).WithMessage("Complaints must not exceed 500 characters.");

        RuleFor(m => m.Symptoms)
            .NotEmpty().WithMessage("Symptoms are required.")
            .MaximumLength(500).WithMessage("Symptoms must not exceed 500 characters.");

        RuleFor(m => m.PatientId)
            .NotEmpty().WithMessage("Patient ID is required.");

        RuleFor(m => m.DoctorId)
            .NotEmpty().WithMessage("Doctor ID is required.");
    }
}
