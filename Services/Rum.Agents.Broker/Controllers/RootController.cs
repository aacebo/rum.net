using Microsoft.AspNetCore.Mvc;

namespace Rum.Agents.Broker.Controllers;

[Route("/")]
[ApiController]
public class RootController : ControllerBase
{
    private readonly DateTime _startedAt = DateTime.Now;

    [HttpGet]
    public IResult Get()
    {
        return Results.Ok(new { started_at = _startedAt });
    }
}