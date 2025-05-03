using FluentValidation;
using MediCore.DTOs.PrescriptionsDTOs;
using System;

namespace MediCore.Validators
{
    public class AddPrescriptionValidator : AbstractValidator<AddPrescriptionsDTO>
    {
        public AddPrescriptionValidator()
        {
            RuleFor(x => x.MedicalRecordId)
                .NotEmpty()
                .WithMessage("Medical Record ID is required.");

            RuleFor(x => x.Status)
                .NotEmpty()
                .WithMessage("Status is required.");

            RuleFor(x => x.ExpiryDate)
                .Must(date => date >= DateOnly.FromDateTime(DateTime.Now))
                .WithMessage("Expiry Date cannot be in the past.");
        }
    }
}
