namespace dotnet_angular_starter.Services.TodoService;

public class TodoService : ITodoService
{
    private readonly IMapper mapper;
    private readonly DataContext dataContext;

    public TodoService(IMapper mapper, DataContext dataContext)
    {
        this.mapper = mapper;
        this.dataContext = dataContext;
    }

    public async Task<ServiceResponse<GetTodoResponse>> CreateTodo(CreateTodoRequest todoRequest)
    {
        Todo mappedTodo = mapper.Map<Todo>(todoRequest);
        await dataContext.AddAsync(mappedTodo);
        await dataContext.SaveChangesAsync();
        var serviceResponse = new ServiceResponse<GetTodoResponse>();
        serviceResponse.Data = mapper.Map<GetTodoResponse>(mappedTodo);
        return serviceResponse;
    }

    public async Task<ServiceResponse<string>> DeleteTodo(int todoId)
    {
        var serviceResponse = new ServiceResponse<string>();
        try
        {
            Todo? dbTodo = await dataContext.Todos.FirstOrDefaultAsync(t => t.Id == todoId);

            if (dbTodo is null)
            {
                throw new Exception($"Todo with Id '{todoId}' is not found!");
            }
            dataContext.Todos.Remove(dbTodo);
            await dataContext.SaveChangesAsync();
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
        var dbTodos = await dataContext.Todos.ToListAsync();
        serviceResponse.Data = dbTodos.Select(mapper.Map<GetTodoResponse>).ToList();
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetTodoResponse>> GetTodo(int todoId)
    {
        var serviceResponse = new ServiceResponse<GetTodoResponse>();
        var dbTodo = await dataContext.Todos.FirstOrDefaultAsync(t => t.Id == todoId)!;
        serviceResponse.Data = mapper.Map<GetTodoResponse>(dbTodo);
        if (dbTodo is null)
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
            Todo? dbTodo = await dataContext.Todos.FirstOrDefaultAsync(t => t.Id == updatedTodo.Id);

            if (dbTodo is null)
            {
                throw new Exception($"Todo with Id '{updatedTodo.Id}' is not found!");
            }

            mapper.Map(updatedTodo, dbTodo);
            await dataContext.SaveChangesAsync();
            var returnTodo = mapper.Map<GetTodoResponse>(dbTodo);
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
