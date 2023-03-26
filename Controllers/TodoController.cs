using Microsoft.AspNetCore.Mvc;

namespace dotnet_angular_starter.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private readonly ITodoService todoService;
    public TodoController(ITodoService todoService)
    {
        this.todoService = todoService;
    }

    [HttpGet("Get/{id}")]
    public async Task<ActionResult<ServiceResponse<Todo>>> GetSingle(int id)
    {
        return Ok(await todoService.GetTodo(id).ConfigureAwait(false));
    }

    [HttpGet("Get")]
    public async Task<ActionResult<ServiceResponse<List<Todo>>>> Get()
    {
        return Ok(await todoService.GetAllTodos().ConfigureAwait(false));
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<List<Todo>>>> CreateTodo(Todo todo)
    {
        return Ok(await todoService.CreateTodo(todo).ConfigureAwait(false));
    }
}
