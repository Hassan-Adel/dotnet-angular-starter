using Microsoft.AspNetCore.Mvc;

namespace dotnet_angular_starter.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HelloController : ControllerBase
{

    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<HelloController> _logger;

    public HelloController(ILogger<HelloController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<string> Get()
    {
        return Enumerable.Range(1, 5).Select(index => Summaries[Random.Shared.Next(Summaries.Length)])
        .ToArray();
    }
}
