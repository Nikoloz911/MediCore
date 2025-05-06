namespace MediCore.DTOs.ChatDTOs;
public class ChatMessageInputDTO
{
    public int DoctorId { get; set; }
    public int PatientId { get; set; }
    public string Message { get; set; }
}
