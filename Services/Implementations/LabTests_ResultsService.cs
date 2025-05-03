using MediCore.Data;
using MediCore.Services.Interfaces;
using AutoMapper;
using MediCore.Core;
using MediCore.DTOs.LabTests_ResultsDTOs;
using MediCore.Models;
using FluentValidation;
using MediCore.Validators;
using Microsoft.EntityFrameworkCore;
namespace MediCore.Services.Implementations;
public class LabTests_ResultsService : ILabTests_Results
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly IValidator<AddLabTestsDTO> _LabTestsValidator;
    private readonly IValidator<AddLabResultDTO> _LabResultsValidator;
    public LabTests_ResultsService(DataContext context, IMapper mapper, IValidator<AddLabTestsDTO> labTestsValidator, IValidator<AddLabResultDTO> labResultsValidator)
    {
        _context = context;
        _mapper = mapper;
        _LabTestsValidator = labTestsValidator;
        _LabResultsValidator = labResultsValidator;
    }

    // GET LAB TESTS
    public ApiResponse<List<GetLabTestsDTO>> GetAllLabTests()
    {
        var labTests = _context.LabTests.ToList();

        if (labTests == null || labTests.Count == 0)
        {
            return new ApiResponse<List<GetLabTestsDTO>>
            {
                Status = 404,
                Message = "No lab tests found.",
                Data = null
            };
        }
        // MAP TO DTO
        var labTestDTOs = _mapper.Map<List<GetLabTestsDTO>>(labTests);
        return new ApiResponse<List<GetLabTestsDTO>>
        {
            Status = 200,
            Message = "Lab tests retrieved successfully.",
            Data = labTestDTOs
        };
    }
    // ADD LAB TEST
    public ApiResponse<AddLabTestsResponseDTO> AddLabTest(AddLabTestsDTO dto)
    {
        // VALIDATE
        var validationResult = _LabTestsValidator.Validate(dto);
        if (!validationResult.IsValid)
        {
            var errorMessages = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));

            return new ApiResponse<AddLabTestsResponseDTO>
            {
                Status = 400,
                Message = errorMessages,
                Data = null
            };
        }

        // MAP TO ENTITY And ADD TO DB
        var entity = _mapper.Map<LabTest>(dto);
        _context.LabTests.Add(entity);
        _context.SaveChanges();

        // MAP TO DTO
        var responseDTO = _mapper.Map<AddLabTestsResponseDTO>(entity);
        return new ApiResponse<AddLabTestsResponseDTO>
        {
            Status = 200,
            Message = "Lab test added successfully.",
            Data = responseDTO
        };
    }
    // GET ALL LAB RESULTS
    public ApiResponse<AddLabResultsResponseDTO> AddLabResult(AddLabResultDTO dto)
    {
        // VALIDATE
        var validationResult = _LabResultsValidator.Validate(dto);
        if (!validationResult.IsValid)
        {
            var errorMessages = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
            return new ApiResponse<AddLabResultsResponseDTO>
            {
                Status = 400,
                Message = errorMessages,
                Data = null
            };
        }
        // VALIDATE IF PATIENT EXISTS
        var patientExists = _context.Patients.Any(p => p.Id == dto.PatientId);
        if (!patientExists)
        {
            return new ApiResponse<AddLabResultsResponseDTO>
            {
                Status = 404,
                Message = "Patient not found.",
                Data = null
            };
        }
        // VALIDATE IF LAB TEST EXISTS
        var labTestExists = _context.LabTests.Any(l => l.Id == dto.LabTestId);
        if (!labTestExists)
        {
            return new ApiResponse<AddLabResultsResponseDTO>
            {
                Status = 404,
                Message = "Lab test not found.",
                Data = null
            };
        }
        // Map DTO
        var entity = _mapper.Map<LabResult>(dto);
        _context.LabResults.Add(entity);
        _context.SaveChanges();
        // MAP TO DTO
        var responseDTO = _mapper.Map<AddLabResultsResponseDTO>(entity);
        return new ApiResponse<AddLabResultsResponseDTO>
        {
            Status = 200,
            Message = "Lab result added successfully.",
            Data = responseDTO
        };
    }
    // GET PATIENT LAB RESULTS
    public ApiResponse<List<GetPatientLabResults>> GetPatientLabResults(int patientId)
    {
        var labResults = _context.LabResults
            .Where(lr => lr.PatientId == patientId)
            .Include(lr => lr.Patient) 
            .Include(lr => lr.LabTest) 
            .ToList();  
        if (labResults.Count == 0)
        {
            return new ApiResponse<List<GetPatientLabResults>>
            {
                Status = 404,
                Message = "No lab results found for this patient.",
                Data = null
            };
        }
        // MAP TO DTO
        var labResultsDTO = _mapper.Map<List<GetPatientLabResults>>(labResults);
        return new ApiResponse<List<GetPatientLabResults>>
        {
            Status = 200,
            Message = "Lab results retrieved successfully.",
            Data = labResultsDTO
        };
    }
}