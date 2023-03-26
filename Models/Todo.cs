namespace dotnet_angular_starter.Models;

public class Todo
{
    public int Id { get; set; }
    public string Title { get; set; } = String.Empty;
    public bool completed { get; set; }
    public TodoType Type { get; set; } = TodoType.Private;
}
