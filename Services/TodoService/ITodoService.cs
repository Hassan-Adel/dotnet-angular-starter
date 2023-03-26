namespace dotnet_angular_starter.Services.TodoService;

public interface ITodoService
{
    List<Todo> GetAllTodos();
    Todo GetTodo(int todoId);
    Todo CreateTodo(Todo todo);
    void DeleteTodo(int todoId);
}
