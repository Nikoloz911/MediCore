using AutoMapper;
using FluentValidation;
using MediCore.Core;
using MediCore.Data;
using MediCore.Models;
using PdfSharpCore;
using PdfSharpCore.Drawing;
using MediCore.DTOs.DiagnosesDTOs;
using MediCore.DTOs.PrescriptionsDTOs;
using MediCore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using MediCore.Enums;
using MediCore.Validators;
using MediCore.SMTP;
using PdfSharpCore.Pdf;

namespace MediCore.Services.Implementations;
public class PrescriptionsService : IPrescriptions
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly IValidator<AddPrescriptionsDTO> _addPrescriptionValidator;
    private readonly IValidator<UpdatePrescriptionDTO> _updatePrescriptionValidator;
    public PrescriptionsService(DataContext context, IMapper mapper, IValidator<AddPrescriptionsDTO> addPrescriptionValidator, IValidator<UpdatePrescriptionDTO> updatePrescriptionValidator)
    {
        _context = context;
        _mapper = mapper;
        _addPrescriptionValidator = addPrescriptionValidator;
        _updatePrescriptionValidator = updatePrescriptionValidator;
    }

    // GET PRESCTIPTIONS BY PATIENT ID
    public ApiResponse<List<GetPatientPrescriptionsDTO>> GetPrescriptionsByPatientId(int patientId)
    {
        var prescriptions = _context.Prescriptions
            .Where(p => p.MedicalRecord.PatientId == patientId)
            .Include(p => p.MedicalRecord)  
            .ThenInclude(mr => mr.Patient) 
            .ThenInclude(p => p.User)     
            .ToList();

        if (!prescriptions.Any())
        {
            return new ApiResponse<List<GetPatientPrescriptionsDTO>>
            {
                Status = 404,
                Message = "No prescriptions found for the given patient ID.",
                Data = null!
            };
        }
        // MAP TO DTO
        var result = _mapper.Map<List<GetPatientPrescriptionsDTO>>(prescriptions);
        return new ApiResponse<List<GetPatientPrescriptionsDTO>>
        {
            Status = 200,
            Message = "Prescriptions retrieved successfully.",
            Data = result
        };
    }
    // GET PRESCTIPTIONS BY ID
    public ApiResponse<GetPrescriptionsByIdDTO> GetPrescriptionById(int id)
    {
        var prescription = _context.Prescriptions
            .Include(p => p.MedicalRecord)
            .FirstOrDefault(p => p.Id == id);

        if (prescription == null)
        {
            return new ApiResponse<GetPrescriptionsByIdDTO>
            {
                Status = 404,
                Message = "Prescription not found.",
                Data = null!
            };
        }
        // MAP TO DTO
        var dto = _mapper.Map<GetPrescriptionsByIdDTO>(prescription);
        return new ApiResponse<GetPrescriptionsByIdDTO>
        {
            Status = 200,
            Message = "Prescription retrieved successfully.",
            Data = dto
        };
    }
    // ADD NEW PRESCRIPTION
    public ApiResponse<AddPrescriptionsResponseDTO> AddPrescription(AddPrescriptionsDTO dto)
    {
        // VALIDATE
        if (dto == null || dto.MedicalRecordId <= 0)
        {
            return new ApiResponse<AddPrescriptionsResponseDTO>
            {
                Status = 400,
                Message = "Invalid input!",
                Data = null!
            };
        }
        // VALIDATE DTO WITH VALIDATOR
        var validationResult = _addPrescriptionValidator.Validate(dto);
        if (!validationResult.IsValid)
        {
            return new ApiResponse<AddPrescriptionsResponseDTO>
            {
                Status = 400,
                Message = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)),
                Data = null!
            };
        }
        // VALIDATE MEDICAL RECORD ID
        var medicalRecord = _context.MedicalRecords
                         .Include(m => m.Patient)
                         .ThenInclude(p => p.User)
                         .FirstOrDefault(m => m.Id == dto.MedicalRecordId);

        if (medicalRecord == null)
        {
            return new ApiResponse<AddPrescriptionsResponseDTO>
            {
                Status = 404,
                Message = "Medical record not found.",
                Data = null!
            };
        }
        // VALIDATE STATUS
        if (!Enum.TryParse<PRESCRIPTION_STATUS>(dto.Status, true, out var status))
        {
            return new ApiResponse<AddPrescriptionsResponseDTO>
            {
                Status = 400,
                Message = "Invalid status value.",
                Data = null!
            };
        }
        // MAP TO PRESCRIPTION MODEL
        var prescription = _mapper.Map<Prescription>(dto);
        prescription.IssueDate = DateOnly.FromDateTime(DateTime.Now);
        _context.Prescriptions.Add(prescription);
        _context.SaveChanges();
        // SEND EMAIL WITH PDF FILE
        var patientName = medicalRecord.Patient.User.FirstName;
        var patientEmail = medicalRecord.Patient.User.Email;
        var pdfPath = GeneratePrescriptionPdf(prescription, patientName);

        SMTP_Prescription.SendPrescriptionEmailWithAttachment(
            patientEmail,
            "New Prescription Issued",
            $"Dear {patientName},<br/>A new prescription has been issued to you. Please find the attached PDF.",
            pdfPath
        );

        // MAP FOR RESPONSE
        var responseDto = _mapper.Map<AddPrescriptionsResponseDTO>(prescription);
        return new ApiResponse<AddPrescriptionsResponseDTO>
        {
            Status = 200,
            Message = "Prescription created successfully.",
            Data = responseDto
        };
    }

    // GENERATE PDF FILE FOR PRESCRIPTION
    private string GeneratePrescriptionPdf(Prescription prescription, string patientName)
    {
        string fileName = $"prescription_{prescription.Id}.pdf";
        string filePath = Path.Combine(AppContext.BaseDirectory, fileName);

        var document = new PdfDocument();
        var page = document.AddPage();
        var gfx = XGraphics.FromPdfPage(page);
        var font = new XFont("Arial", 12, XFontStyle.Regular);
        var boldFont = new XFont("Arial", 14, XFontStyle.Bold);

        int yPoint = 40;
        gfx.DrawString("Prescription Details", boldFont, XBrushes.Black, new XRect(0, yPoint, page.Width, page.Height), XStringFormats.TopCenter);
        yPoint += 40;

        gfx.DrawString($"Prescription ID: {prescription.Id}", font, XBrushes.Black, 20, yPoint); yPoint += 25;
        gfx.DrawString($"Patient Name: {patientName}", font, XBrushes.Black, 20, yPoint); yPoint += 25;
        gfx.DrawString($"Issue Date: {prescription.IssueDate}", font, XBrushes.Black, 20, yPoint); yPoint += 25;
        gfx.DrawString($"Expiry Date: {prescription.ExpiryDate}", font, XBrushes.Black, 20, yPoint); yPoint += 25;
        gfx.DrawString($"Status: {prescription.Status}", font, XBrushes.Black, 20, yPoint); yPoint += 25;

        document.Save(filePath);
        return filePath;
    }

    // BACKGROUND JOB TO UPDATE EXPIRED PRESCRIPTIONS
    public void UpdateExpiredPrescriptions()
    {
        var today = DateOnly.FromDateTime(DateTime.Now);

        var expiredPrescriptions = _context.Prescriptions
            .Where(p => p.ExpiryDate < today && p.Status == PRESCRIPTION_STATUS.ACTIVE)
            .ToList();

        foreach (var prescription in expiredPrescriptions)
        {
            prescription.Status = PRESCRIPTION_STATUS.INACTIVE;
        }
        _context.SaveChanges();
    }

    // UPDATE PRESCRIPTION
    public ApiResponse<UpdatePrescriptionResponseDTO> UpdatePrescription(int id, UpdatePrescriptionDTO dto)
    {
        // VALIDATE
        if (dto.MedicalRecordId <= 0)
        {
            return new ApiResponse<UpdatePrescriptionResponseDTO>
            {
                Status = 400,
                Message = "Invalid input!",
                Data = null!
            };
        }
        // VALIDATE DTO WITH VALIDATOR
        var validationResult = _updatePrescriptionValidator.Validate(dto);
        if (!validationResult.IsValid)
        {
            return new ApiResponse<UpdatePrescriptionResponseDTO>
            {
                Status = 400,
                Message = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)),
                Data = null!
            };
        }
        // VALIDATE MEDICAL RECORD ID
        var prescription = _context.Prescriptions.FirstOrDefault(p => p.Id == id);
        if (prescription == null)
        {
            return new ApiResponse<UpdatePrescriptionResponseDTO>
            {
                Status = 404,
                Message = "Prescription not found.",
                Data = null!
            };
        }
        // VALIDATE MEDCIAL RECORD ID
        var medicalRecord = _context.MedicalRecords.FirstOrDefault(m => m.Id == dto.MedicalRecordId);
        if (medicalRecord == null)
        {
            return new ApiResponse<UpdatePrescriptionResponseDTO>
            {
                Status = 404,
                Message = "Medical record not found.",
                Data = null!
            };
        }
        // VALIDATE STATUS
        if (!Enum.TryParse<PRESCRIPTION_STATUS>(dto.Status, true, out var status))
        {
            return new ApiResponse<UpdatePrescriptionResponseDTO>
            {
                Status = 400,
                Message = "Invalid status value.",
                Data = null!
            };
        }
        prescription.Status = status;
        // MAP TO PRESCRIPTION MODEL
        _mapper.Map(dto, prescription);
        _context.SaveChanges();
        // MAP FOR RESPONSE
        var response = _mapper.Map<UpdatePrescriptionResponseDTO>(prescription);
        return new ApiResponse<UpdatePrescriptionResponseDTO>
        {
            Status = 200,
            Message = "Prescription updated successfully.",
            Data = response
        };
    }
    // GET ACTIVE PRESCRIPTIONS BY PATIENT ID
    public ApiResponse<List<GetActivePrescriptionsDTO>> GetActivePrescriptionsByPatientId(int patientId)
    {
        var today = DateOnly.FromDateTime(DateTime.Now);
        var prescriptions = _context.Prescriptions
            .Where(p => p.MedicalRecord.Patient.Id == patientId &&
                        p.Status == PRESCRIPTION_STATUS.ACTIVE &&
                        p.ExpiryDate >= today)
            .ToList();

        if (!prescriptions.Any())
        {
            return new ApiResponse<List<GetActivePrescriptionsDTO>>
            {
                Status = 404,
                Message = "No active prescriptions found for this patient.",
                Data = new List<GetActivePrescriptionsDTO>()
            };
        }
        // MAP TO DTO
        var mappedPrescriptions = prescriptions
            .Select(p => _mapper.Map<GetActivePrescriptionsDTO>(p))
            .ToList();

        return new ApiResponse<List<GetActivePrescriptionsDTO>>
        {
            Status = 200,
            Message = "Active prescriptions retrieved successfully.",
            Data = mappedPrescriptions
        };
    }
    // ADD PRESCRIPTION ITEM
    public ApiResponse<AddPrescriptionItemResponseDTO> AddPrescriptionItem(AddPrescriptionItemDTO dto)
    {
        // Validate DTO input
        if (dto == null || dto.PrescriptionId <= 0 || dto.MedicationId <= 0 || string.IsNullOrEmpty(dto.DosageInstructions))
        {
            return new ApiResponse<AddPrescriptionItemResponseDTO>
            {
                Status = 400,
                Message = "Invalid input!",
                Data = null
            };
        }
        // Validate DosageInstructions length
        if (dto.DosageInstructions.Length > 150)
        {
            return new ApiResponse<AddPrescriptionItemResponseDTO>
            {
                Status = 400,
                Message = "Dosage Instructions cannot exceed 150 characters.",
                Data = null
            };
        }
        // Validate DurationDays value
        if (dto.DurationDays <= 0 || dto.DurationDays > 3065)
        {
            return new ApiResponse<AddPrescriptionItemResponseDTO>
            {
                Status = 400,
                Message = "Duration Days must be a positive number and should not exceed 3065 days.",
                Data = null
            };
        }
        // VALIDATE PRESCRIPTION ID
        var prescription = _context.Prescriptions.FirstOrDefault(p => p.Id == dto.PrescriptionId);
        if (prescription == null)
        {
            return new ApiResponse<AddPrescriptionItemResponseDTO>
            {
                Status = 404,
                Message = "Prescription not found.",
                Data = null
            };
        }
        // VALIDATE MEDICATION ID
        var medication = _context.Medications.FirstOrDefault(m => m.Id == dto.MedicationId);
        if (medication == null)
        {
            return new ApiResponse<AddPrescriptionItemResponseDTO>
            {
                Status = 404,
                Message = "Medication not found.",
                Data = null
            };
        }
        // MAP TO PRESCRIPTION ITEM
        var prescriptionItem = _mapper.Map<PrescriptionItem>(dto);
        _context.PrescriptionItems.Add(prescriptionItem);
        _context.SaveChanges();
        // MAP FOR RESPONSE
        var responseDto = _mapper.Map<AddPrescriptionItemResponseDTO>(prescriptionItem);
        return new ApiResponse<AddPrescriptionItemResponseDTO>
        {
            Status = 200,
            Message = "Prescription item added successfully.",
            Data = responseDto
        };
    }
}