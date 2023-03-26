namespace dotnet_angular_starter.Models;

public class ServiceResponse<T>
{
    public T? Data { get; set; }
    public bool Successful { get; set; } = true;
    public string Message { get; set; } = String.Empty;
}
