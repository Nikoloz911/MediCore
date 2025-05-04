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
        // ADD DEPARTMNET
        public ApiResponse<DepartmentAllDTO> CreateDepartment(DepartmentAddDTO departmentDto)
        {
            if (string.IsNullOrWhiteSpace(departmentDto.DepartmentType) || departmentDto.DepartmentType.Length > 20)
            {
                return new ApiResponse<DepartmentAllDTO>
                {
                    Status = 400,
                    Message = "Department type must be a non-empty string with max length 20.",
                    Data = null
                };
            }

            var department = new Department
            {
                DepartmentType = departmentDto.DepartmentType.Trim(),
                Doctors = new List<Doctor>(),
                Appointments = new List<Appointment>()
            };

            _context.Departments.Add(department);
            _context.SaveChanges();

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
            if (string.IsNullOrEmpty(departmentDto.DepartmentType) || departmentDto.DepartmentType.Length > 50)
            {
                return new ApiResponse<DepartmentAllDTO>
                {
                    Status = 400,
                    Message = "Department type must be a non-empty string with max length 50.",
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
            department.DepartmentType = departmentDto.DepartmentType;

            _context.SaveChanges();
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