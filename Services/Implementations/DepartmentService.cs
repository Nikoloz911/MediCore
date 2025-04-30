using AutoMapper;
using MediCore.Core;
using MediCore.Data;
using MediCore.DTOs.DepartmentsDTOs;
using MediCore.Enums;
using MediCore.Models;
using MediCore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MediCore.Services.Implementations
{
    public class DepartmentService : IDepartment
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DepartmentService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET ALL DEPARTMENTS
        public ApiResponse<List<DepartmentAllDTO>> GetAllDepartments()
        {
            var departments = _context.Departments.ToList();
            if (!departments.Any())
            {
                return new ApiResponse<List<DepartmentAllDTO>>
                {
                    Status = 404,
                    Message = "No departments found.",
                    Data = null
                };
            }

            var departmentDtos = _mapper.Map<List<DepartmentAllDTO>>(departments);
            return new ApiResponse<List<DepartmentAllDTO>>
            {
                Status = 200,
                Message = "Departments retrieved successfully.",
                Data = departmentDtos
            };
        }
        // ADD NEW DEPARTMENT
        public ApiResponse<DepartmentAllDTO> CreateDepartment(DepartmentAddDTO departmentDto)
        {
            if (string.IsNullOrEmpty(departmentDto.DepartmentName) || departmentDto.DepartmentName.Length > 20)
            {
                return new ApiResponse<DepartmentAllDTO>
                {
                    Status = 400,
                    Message = "Department name must be a non-empty string with max length 20.",
                    Data = null
                };
            }

            var existingDepartment = _context.Departments
                .FirstOrDefault(d => d.DepartmentName.ToLower() == departmentDto.DepartmentName.ToLower());
            if (existingDepartment != null)
            {
                return new ApiResponse<DepartmentAllDTO>
                {
                    Status = 409,
                    Message = "Department with this name already exists.",
                    Data = null
                };
            }

            DEPARTMENT_TYPE? matchedEnum = null;
            if (Enum.TryParse<DEPARTMENT_TYPE>(departmentDto.DepartmentName, true, out var parsedEnum))
            {
                matchedEnum = parsedEnum;
            }

            var department = new Department
            {
                DepartmentName = departmentDto.DepartmentName,
                DepartmentType = matchedEnum,
                Doctors = new List<Doctor>(),
                Appointments = new List<Appointment>()
            };
            _context.Departments.Add(department);
            _context.SaveChanges();
            // Map the created department to DTO
            var createdDto = _mapper.Map<DepartmentAllDTO>(department);
            return new ApiResponse<DepartmentAllDTO>
            {
                Status = 200,
                Message = "Department created successfully.",
                Data = createdDto
            };
        }


        // UPDATE DEPARTMENT
        public ApiResponse<DepartmentAllDTO> UpdateDepartment(int id, DepartmentUpdateDTO departmentDto)
        {
            if (string.IsNullOrEmpty(departmentDto.DepartmentName) || departmentDto.DepartmentName.Length > 20)
            {
                return new ApiResponse<DepartmentAllDTO>
                {
                    Status = 400,
                    Message = "Department name must be a non-empty string with max length 20.",
                    Data = null
                };
            }

            var department = _context.Departments.FirstOrDefault(d => d.Id == id);
            if (department == null)
            {
                return new ApiResponse<DepartmentAllDTO>
                {
                    Status = 404,
                    Message = "Department not found.",
                    Data = null
                };
            }
            // VALIDATE IF DEPARTMENT NAME OR TYPE ALREADY EXISTS WITH ENUM
            var nameToCheck = departmentDto.DepartmentName.Trim().ToLower();
            var nameExists = _context.Departments.Any(d =>
                d.Id != id &&
                d.DepartmentName != null &&
                d.DepartmentName.ToLower() == nameToCheck
            );
            bool enumExists = false;
            if (Enum.TryParse<DEPARTMENT_TYPE>(departmentDto.DepartmentName, true, out var parsedEnum))
            {
                enumExists = _context.Departments.Any(d =>
                    d.Id != id &&
                    d.DepartmentType == parsedEnum
                );
            }
            if (nameExists || enumExists)
            {
                return new ApiResponse<DepartmentAllDTO>
                {
                    Status = 409,
                    Message = "A department with this name or type already exists.",
                    Data = null
                };
            }
            department.DepartmentName = departmentDto.DepartmentName;
            department.DepartmentType = Enum.TryParse<DEPARTMENT_TYPE>(departmentDto.DepartmentName, true, out var type)
                ? type
                : null;

            _context.SaveChanges();
            // MAP THE UPDATED DEPARTMENT TO DTO
            var updatedDto = _mapper.Map<DepartmentAllDTO>(department);
            return new ApiResponse<DepartmentAllDTO>
            {
                Status = 200,
                Message = "Department updated successfully.",
                Data = updatedDto
            };
        }


        // DELETE DEPARTMENT
        public ApiResponse<bool> DeleteDepartment(int id)
        {
            var department = _context.Departments
                .FirstOrDefault(d => d.Id == id);

            if (department == null)
            {
                return new ApiResponse<bool>
                {
                    Status = 404,
                    Message = "Department not found.",
                    Data = false
                };
            }

            _context.Departments.Remove(department);
            _context.SaveChanges();

            return new ApiResponse<bool>
            {
                Status = 200,
                Message = "Department deleted successfully.",
                Data = true
            };
        }
    }
}