using Microsoft.AspNetCore.Mvc;

namespace dotnet_angular_starter.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private static readonly Todo todo = new Todo();
    private static readonly List<Todo> todos = new List<Todo>{
        todo,
        new Todo(){Id = 1, Title = "test todo"}
    };

    [HttpGet("Get/{id}")]
    public ActionResult<Todo> GetSingle(int id)
    {
        return Ok(todos.FirstOrDefault(t => t.Id == id));
    }
    
    [HttpGet("Get")]
    public ActionResult<List<Todo>> Get()
    {
        return Ok(todos);
    }

    [HttpPost]
    public ActionResult<List<Todo>> CreateTodo(Todo todo)
    {
        todos.Add(todo);
        return Ok(todos);
    }
}
