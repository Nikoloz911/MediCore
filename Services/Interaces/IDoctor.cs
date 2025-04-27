using MediCore.Core;
using MediCore.DTOs.DoctorDTOs;

namespace MediCore.Services.Interaces;
public interface IDoctor
{
    public ApiResponse<List<DoctorDTO>> GetAllDoctors();
}
