using Microsoft.AspNetCore.Mvc;

using Rum.Agents.Broker.Models;
using Rum.Agents.Broker.Storage;

namespace Rum.Agents.Broker.Controllers;

[Route("/agents")]
[ApiController]
public class AgentController : ControllerBase
{
    private readonly IAgentStorage _storage;

    public AgentController(IAgentStorage storage)
    {
        _storage = storage;
    }

    [HttpGet]
    public async Task<IResult> Get(CancellationToken cancellationToken = default)
    {
        var agents = await _storage.Get(cancellationToken);
        return Results.Ok(agents);
    }

    [HttpGet("{name}")]
    public async Task<IResult> GetByName(string name, CancellationToken cancellationToken = default)
    {
        var agent = await _storage.GetByName(name, cancellationToken);
        return agent == null ? Results.NotFound() : Results.Ok(agent);
    }

    [HttpPost]
    public async Task<IResult> Create([FromBody] Agent.CreateRequest request, CancellationToken cancellationToken = default)
    {
        var agent = await _storage.GetByName(request.Name, cancellationToken);

        if (agent != null)
        {
            return Results.Conflict("duplicate agent name");
        }

        agent = await _storage.Create(new()
        {
            Name = request.Name,
            Version = request.Version,
            Description = request.Description,
            Url = request.Url,
            DocumentationUrl = request.DocumentationUrl
        }, cancellationToken);

        return Results.Created($"/agents/{agent.Name}", agent);
    }
}