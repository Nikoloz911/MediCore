using MediCore.Enums;
namespace MediCore.DTOs.PrescriptionsDTOs;
public class GetActivePrescriptionsDTO
{
    public int Id { get; set; }
    public int MedicalRecordId { get; set; }
    public DateOnly IssueDate { get; set; }
    public DateOnly ExpiryDate { get; set; }
    public PRESCRIPTION_STATUS Status { get; set; }
}
