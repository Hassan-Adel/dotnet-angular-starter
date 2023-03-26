namespace dotnet_angular_starter.Services.TodoService;

public interface ITodoService
{
    Task<ServiceResponse<List<Todo>>> GetAllTodos();
    Task<ServiceResponse<Todo>> GetTodo(int todoId);
    Task<ServiceResponse<Todo>> CreateTodo(Todo todo);
    void DeleteTodo(int todoId);
}
