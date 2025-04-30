using MediCore.Core;
using MediCore.DTOs.DepartmentsDTOs;

namespace MediCore.Services.Interfaces;
public interface IDepartment
{
    ApiResponse<List<DepartmentAllDTO>> GetAllDepartments();
    ApiResponse<DepartmentAllDTO> CreateDepartment(DepartmentAddDTO departmentDto);
    ApiResponse<DepartmentAllDTO> UpdateDepartment(int id, DepartmentUpdateDTO departmentDto);
    ApiResponse<bool> DeleteDepartment(int id);
}
