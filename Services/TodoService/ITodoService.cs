namespace dotnet_angular_starter.Services.TodoService;

public interface ITodoService
{
    Task<ServiceResponse<List<GetTodoResponse>>> GetAllTodos();
    Task<ServiceResponse<GetTodoResponse>> GetTodo(int todoId);
    Task<ServiceResponse<GetTodoResponse>> CreateTodo(CreateTodoRequest todo);
    Task<ServiceResponse<GetTodoResponse>> UpdateTodo(UpdateTodoRequest todo);
    Task<ServiceResponse<string>> DeleteTodo(int todoId);
}
