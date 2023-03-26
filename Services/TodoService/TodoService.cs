namespace dotnet_angular_starter.Services.TodoService;

public class TodoService : ITodoService
{
    private static readonly Todo todo = new Todo();
    private static readonly List<Todo> todos = new List<Todo>{
        todo,
        new Todo(){Id = 1, Title = "test todo"}
    };

    public async Task<ServiceResponse<Todo>> CreateTodo(Todo todo)
    {
        todos.Add(todo);
        var serviceResponse = new ServiceResponse<Todo>();
        serviceResponse.Data = todo;
        return serviceResponse;
    }

    public void DeleteTodo(int todoId)
    {
        var todoToDelete = todos.FirstOrDefault(t => t.Id == todoId);

        if(todoToDelete is null)
            throw new Exception("Todo not found!");

        todos.Remove(todoToDelete);
    }

    public async Task<ServiceResponse<List<Todo>>> GetAllTodos()
    {
        var serviceResponse = new ServiceResponse<List<Todo>>();
        serviceResponse.Data = todos;
        return serviceResponse;
    }

    public async Task<ServiceResponse<Todo>> GetTodo(int todoId)
    {
        var serviceResponse = new ServiceResponse<Todo>();
        Todo todo = todos.FirstOrDefault(t => t.Id == todoId)!;
        serviceResponse.Data = todo;
        if(todo is null) {
            serviceResponse.Successful = false;
            serviceResponse.Message = "Todo not found!";
        }
        
        return serviceResponse;
    }
}
