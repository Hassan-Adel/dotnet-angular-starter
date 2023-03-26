namespace dotnet_angular_starter.Services.TodoService;

public class TodoService : ITodoService
{
    private static readonly Todo todo = new Todo();
    private static readonly List<Todo> todos = new List<Todo>{
        todo,
        new Todo(){Id = 1, Title = "test todo"}
    };
    private readonly IMapper mapper;
    public TodoService(IMapper mapper)
    {
        this.mapper = mapper;
    }

    public async Task<ServiceResponse<GetTodoResponse>> CreateTodo(CreateTodoRequest todoRequest)
    {

        Todo mappedTodo = mapper.Map<Todo>(todoRequest);
        todos.Add(mappedTodo);
        var serviceResponse = new ServiceResponse<GetTodoResponse>();
        serviceResponse.Data = mapper.Map<GetTodoResponse>(mappedTodo);
        return serviceResponse;
    }

    public async Task<ServiceResponse<string>> DeleteTodo(int todoId)
    {
        var serviceResponse = new ServiceResponse<string>();
        try
        {
            Todo todo = todos.FirstOrDefault(t => t.Id == todoId)!;

            if (todo is null)
            {
                throw new Exception($"Todo with Id '{todoId}' is not found!");
            }
            todos.Remove(todo);
            serviceResponse.Data = $"Todo with Id '{todoId}' has been successfully delete!";
        }
        catch (Exception ex)
        {
            serviceResponse.Successful = false;
            serviceResponse.Message = ex.Message;
        }
        return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetTodoResponse>>> GetAllTodos()
    {
        var serviceResponse = new ServiceResponse<List<GetTodoResponse>>();
        serviceResponse.Data = todos.Select(mapper.Map<GetTodoResponse>).ToList();
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetTodoResponse>> GetTodo(int todoId)
    {
        var serviceResponse = new ServiceResponse<GetTodoResponse>();
        Todo todo = todos.FirstOrDefault(t => t.Id == todoId)!;
        serviceResponse.Data = mapper.Map<GetTodoResponse>(todo);
        if (todo is null)
        {
            serviceResponse.Successful = false;
            serviceResponse.Message = "Todo not found!";
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<GetTodoResponse>> UpdateTodo(UpdateTodoRequest updatedTodo)
    {
        var serviceResponse = new ServiceResponse<GetTodoResponse>();
        try
        {
            Todo todo = todos.FirstOrDefault(t => t.Id == updatedTodo.Id)!;

            if (todo is null)
            {
                throw new Exception($"Todo with Id '{updatedTodo.Id}' is not found!");
            }

            mapper.Map(updatedTodo, todo);
            var returnTodo = mapper.Map<GetTodoResponse>(todo);
            serviceResponse.Data = returnTodo;
        }
        catch (Exception ex)
        {
            serviceResponse.Successful = false;
            serviceResponse.Message = ex.Message;
        }
        return serviceResponse;
    }
}
