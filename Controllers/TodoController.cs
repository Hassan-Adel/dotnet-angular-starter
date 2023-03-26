using Microsoft.AspNetCore.Mvc;

namespace dotnet_angular_starter.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private static readonly Todo todo = new Todo();

    [HttpGet]
    public ActionResult<Todo> Get()
    {
        return Ok(todo);
    }
}
