using Microsoft.AspNetCore.Mvc;

namespace Mcp.Broker.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RootController : ControllerBase
{
    public readonly DateTime StartedAt = DateTime.Now;

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { hello = "world!" });
    }
}