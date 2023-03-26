namespace dotnet_angular_starter.Dtos.Todo;

public class CreateTodoRequest
{
    public string Title { get; set; } = String.Empty;
    public bool Completed { get; set; }
    public TodoType Type { get; set; } = TodoType.Private;
}
