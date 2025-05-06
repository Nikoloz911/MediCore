namespace MediCore.Models;
public class ChatMessage
{
    public int Id { get; set; }
    public int DoctorId { get; set; }
    public int PatientId { get; set; }
    public string Message { get; set; }
    public DateTime Timestamp { get; set; }
    public Doctor Doctor { get; set; }
    public Patient Patient { get; set; }
}
