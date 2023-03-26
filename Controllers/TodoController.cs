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
    public ActionResult<Todo> GetSingle(int id)
    {
        return Ok(todoService.GetTodo(id));
    }
    
    [HttpGet("Get")]
    public ActionResult<List<Todo>> Get()
    {
        return Ok(todoService.GetAllTodos());
    }

    [HttpPost]
    public ActionResult<List<Todo>> CreateTodo(Todo todo)
    {
        return Ok(todoService.CreateTodo(todo));
    }
}
