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
        public ApiResponse<DepartmentAllDTO> CreateDepartment(DepartmentAddDTO departmentDto)
        {
            // Validate Department Type Length
            if (string.IsNullOrEmpty(departmentDto.DepartmentType) || departmentDto.DepartmentType.Length > 20)
            {
                return new ApiResponse<DepartmentAllDTO>
                {
                    Status = 400,
                    Message = "Department type must be a non-empty string with a maximum length of 20 characters.",
                    Data = null
                };
            }

            // Validate Enum Type
            if (!Enum.TryParse<DEPARTMENT_TYPE>(departmentDto.DepartmentType, true, out DEPARTMENT_TYPE departmentType))
            {
                return new ApiResponse<DepartmentAllDTO>
                {
                    Status = 400,
                    Message = "Invalid department type.",
                    Data = null
                };
            }

            var existingDepartment = _context.Departments
                .FirstOrDefault(d => d.DepartmentType == departmentType);

            if (existingDepartment != null)
            {
                return new ApiResponse<DepartmentAllDTO>
                {
                    Status = 409,  // Changed to 409 for conflict
                    Message = "Department with this type already exists.",
                    Data = null
                };
            }

            var department = new Department
            {
                DepartmentType = departmentType,
                Doctors = new List<Doctor>(),
                Appointments = new List<Appointment>()
            };

            _context.Departments.Add(department);
            _context.SaveChanges();

            var createdDepartmentDto = _mapper.Map<DepartmentAllDTO>(department);

            return new ApiResponse<DepartmentAllDTO>
            {
                Status = 201,
                Message = "Department created successfully.",
                Data = createdDepartmentDto
            };
        }

        public ApiResponse<DepartmentAllDTO> UpdateDepartment(int id, DepartmentUpdateDTO departmentDto)
        {
            // Validate Department Type Length
            if (string.IsNullOrEmpty(departmentDto.DepartmentType) || departmentDto.DepartmentType.Length > 20)
            {
                return new ApiResponse<DepartmentAllDTO>
                {
                    Status = 400,
                    Message = "Department type must be a non-empty string with a maximum length of 20 characters.",
                    Data = null
                };
            }

            // Validate Enum Type
            if (!Enum.TryParse<DEPARTMENT_TYPE>(departmentDto.DepartmentType, true, out DEPARTMENT_TYPE departmentType))
            {
                return new ApiResponse<DepartmentAllDTO>
                {
                    Status = 400,
                    Message = "Invalid department type.",
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

            var existingDepartment = _context.Departments
                .FirstOrDefault(d => d.DepartmentType == departmentType && d.Id != id);

            if (existingDepartment != null)
            {
                return new ApiResponse<DepartmentAllDTO>
                {
                    Status = 409,  // Changed to 409 for conflict
                    Message = "Another department with this type already exists.",
                    Data = null
                };
            }

            department.DepartmentType = departmentType;
            _context.SaveChanges();

            var updatedDepartmentDto = _mapper.Map<DepartmentAllDTO>(department);

            return new ApiResponse<DepartmentAllDTO>
            {
                Status = 200,
                Message = "Department updated successfully.",
                Data = updatedDepartmentDto
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