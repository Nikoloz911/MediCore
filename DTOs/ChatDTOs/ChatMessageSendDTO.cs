namespace MediCore.DTOs.ChatDTOs;
public class ChatMessageSendDTO
{
    public int Id { get; set; }
    public int DoctorId { get; set; }
    public int PatientId { get; set; }
    public string Message { get; set; }
    public DateTime Timestamp { get; set; }
}
