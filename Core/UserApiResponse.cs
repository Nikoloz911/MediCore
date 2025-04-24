namespace MediCore.Core;
public class UserApiResponse<T>
{
    public int Status { get; set; }
    public string? Message { get; set; }
    public T Data { get; set; }
}
