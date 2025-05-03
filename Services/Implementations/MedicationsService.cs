using AutoMapper;
using FluentValidation;
using MediCore.Core;
using MediCore.Data;
using MediCore.DTOs.MedicationsDTOs;
using MediCore.Models;
using FluentValidation.Results;
using MediCore.Services.Interfaces;
using MediCore.Validators;

namespace MediCore.Services.Implementations;
public class MedicationsService : IMedications
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly IValidator<AddMedicationDTO> _validator;
    public MedicationsService(DataContext context, IMapper mapper, IValidator<AddMedicationDTO> validator)
    {
        _context = context;
        _mapper = mapper;
        _validator = validator;
    }
    // GET ALL MEDICATIONS WITH FILTERS
    public ApiResponse<List<GetAllMedications>> GetMedications(
    string? name = null,
    string? activeSubstance = null,
    string? category = null,
    string? form = null)
    {
        var query = _context.Medications.AsQueryable();

        if (!string.IsNullOrWhiteSpace(name))
            query = query.Where(m => m.Name.Contains(name));

        if (!string.IsNullOrWhiteSpace(activeSubstance))
            query = query.Where(m => m.ActiveSubstance.Contains(activeSubstance));

        if (!string.IsNullOrWhiteSpace(category))
            query = query.Where(m => m.Category.Contains(category));

        if (!string.IsNullOrWhiteSpace(form))
            query = query.Where(m => m.Form.Contains(form));

        var medications = query.ToList();

        if (!medications.Any())
        {
            return new ApiResponse<List<GetAllMedications>>
            {
                Status = 404,
                Message = "No medications found matching the filters.",
                Data = new List<GetAllMedications>()
            };
        }

        var result = _mapper.Map<List<GetAllMedications>>(medications);

        return new ApiResponse<List<GetAllMedications>>
        {
            Status = 200,
            Message = "Medications fetched successfully.",
            Data = result
        };
    }
    // GET MEDICATION BY ID
    public ApiResponse<GetMedication> GetMedicationById(int id)
    {
        var medication = _context.Medications.FirstOrDefault(m => m.Id == id);

        if (medication == null)
        {
            return new ApiResponse<GetMedication>
            {
                Status = 404,
                Message = $"Medication with ID {id} not found.",
                Data = null!
            };
        }
        // MAP TO DTO
        var dto = _mapper.Map<GetMedication>(medication);

        return new ApiResponse<GetMedication>
        {
            Status = 200,
            Message = "Medication retrieved successfully.",
            Data = dto
        };
    }
    // ADD MEDICATION
    public ApiResponse<AddMedicationResponseDTO> AddMedication(AddMedicationDTO dto)
    {
        // VALIDATE DTO
        ValidationResult result = _validator.Validate(dto);
        if (!result.IsValid)
        {
            var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
            return new ApiResponse<AddMedicationResponseDTO>
            {
                Status = 400,
                Message = string.Join(" | ", errors),
                Data = null!
            };
        }
        // MAP TO MODEL
        var medication = _mapper.Map<Medication>(dto);
        _context.Medications.Add(medication);
        _context.SaveChanges();
        // MAP TO RESPONSE DTO
        var responseDto = _mapper.Map<AddMedicationResponseDTO>(medication);
        return new ApiResponse<AddMedicationResponseDTO>
        {
            Status = 200,
            Message = "Medication created successfully.",
            Data = responseDto
        };
    }
    // UPDATE MEDICATION
    public ApiResponse<AddMedicationResponseDTO> UpdateMedication(int id, UpdateMedicationDTO dto)
    {
        // FIND MEDICATION
        var medication = _context.Medications.FirstOrDefault(m => m.Id == id);
        if (medication == null)
        {
            return new ApiResponse<AddMedicationResponseDTO>
            {
                Status = 404,
                Message = $"Medication with ID {id} not found.",
                Data = null!
            };
        }
        // VALIDATE DTO
        var validator = new UpdateMedicationValidator();
        var validationResult = validator.Validate(dto);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage);
            return new ApiResponse<AddMedicationResponseDTO>
            {
                Status = 400,
                Message = string.Join(" | ", errors),
                Data = null!
            };
        }
        // MAP TO MODEL
        _mapper.Map(dto, medication);
        _context.SaveChanges();
        // MAP TO RESPONSE DTO
        var responseDto = _mapper.Map<AddMedicationResponseDTO>(medication);
        return new ApiResponse<AddMedicationResponseDTO>
        {
            Status = 200,
            Message = "Medication updated successfully.",
            Data = responseDto
        };
    }
}
