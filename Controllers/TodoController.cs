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
    public async Task<ActionResult<ServiceResponse<GetTodoResponse>>> GetSingle(int id)
    {
        return Ok(await todoService.GetTodo(id).ConfigureAwait(false));
    }

    [HttpGet("Get")]
    public async Task<ActionResult<ServiceResponse<List<GetTodoResponse>>>> Get()
    {
        return Ok(await todoService.GetAllTodos().ConfigureAwait(false));
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<List<GetTodoResponse>>>> CreateTodo(CreateTodoRequest todo)
    {
        return Ok(await todoService.CreateTodo(todo).ConfigureAwait(false));
    }

    [HttpPut]
    public async Task<ActionResult<ServiceResponse<List<GetTodoResponse>>>> UpdateTodo(UpdateTodoRequest todo)
    {
        var response = await todoService.UpdateTodo(todo).ConfigureAwait(false);
        if (response.Data is null)
        {
            return NotFound(response);
        }

        return Ok(response);
    }

    [HttpDelete("Delete/{todoId}")]
    public async Task<ActionResult<ServiceResponse<string>>> DeleteTodo(int todoId)
    {
        var response = await todoService.DeleteTodo(todoId).ConfigureAwait(false);
        if (!response.Successful)
        {
            return NotFound(response);
        }

        return Ok(response);
    }
}
