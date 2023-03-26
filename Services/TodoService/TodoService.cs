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

    public void DeleteTodo(int todoId)
    {
        var todoToDelete = todos.FirstOrDefault(t => t.Id == todoId);

        if (todoToDelete is null)
        {
            throw new Exception("Todo not found!");
        }

        todos.Remove(todoToDelete);
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
}
