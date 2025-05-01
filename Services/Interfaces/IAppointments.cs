using MediCore.Core;
using MediCore.DTOs.AppointmentsDTOs;
using MediCore.Enums;

namespace MediCore.Services.Interfaces;
public interface IAppointments
{
        ApiResponse<List<GetAppointmentsDTO>> GetAppointments(
        int? doctorId = null,
        int? patientId = null,
        int? departmentId = null,
        APPOINTMENT_STATUS? status = null,
        APPOINTMENT_TYPE? visitType = null,
        DateTime? date = null
    );
    ApiResponse<GetAppointmentsByIdDTO> GetAppointmentById(int id);
    ApiResponse<AddAppointmentResponseDTO> AddAppointment(AddAppointmentDTO dto);
}
