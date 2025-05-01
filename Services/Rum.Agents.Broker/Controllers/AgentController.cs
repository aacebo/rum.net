using Microsoft.AspNetCore.Mvc;

using Rum.Agents.Broker.Models;
using Rum.Agents.Broker.Resolvers;
using Rum.Agents.Broker.Storage;

namespace Rum.Agents.Broker.Controllers;

[Route("/agents")]
[ApiController]
public class AgentController : ControllerBase
{
    private readonly IAgentStorage _storage;
    private readonly AgentResolver _resolver;

    public AgentController(IAgentStorage storage, AgentResolver resolver)
    {
        _storage = storage;
        _resolver = resolver;
    }

    [HttpGet("$schema")]
    public IResult GetSchema()
    {
        return Results.Ok(_resolver.ToSchema());
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

        if (agent is null) return Results.NotFound();
        if (HttpContext.Request.Query.TryGetValue("q", out var query))
        {
            var res = await _resolver.Resolve(agent, query.ToString());
            return Results.Json(res, statusCode: res.IsError ? 400 : 200);
        }

        return Results.Ok(agent);
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
            DocumentationUrl = request.DocumentationUrl,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        }, cancellationToken);

        return Results.Created($"/agents/{agent.Name}", agent);
    }
}