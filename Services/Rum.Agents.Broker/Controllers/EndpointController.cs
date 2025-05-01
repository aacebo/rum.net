using Microsoft.AspNetCore.Mvc;

using Rum.Agents.Broker.Storage;

namespace Rum.Agents.Broker.Controllers;

[ApiController]
public class EndpointController : ControllerBase
{
    private readonly IAgentStorage _agents;
    private readonly IEndpointStorage _endpoints;

    public EndpointController(IAgentStorage agents, IEndpointStorage storage)
    {
        _agents = agents;
        _endpoints = storage;
    }

    [HttpPost("/agents/{name}/endpoints")]
    public async Task<IResult> Create([FromRoute] string name, [FromBody] Models.Endpoint.CreateRequest request, CancellationToken cancellationToken = default)
    {
        var agent = await _agents.GetByName(name, cancellationToken);

        if (agent is null)
        {
            return Results.NotFound();
        }

        var endpoints = await _endpoints.GetByAgentId(agent.Id, cancellationToken);

        if (endpoints.Any(e => e.Path == request.Path))
        {
            return Results.Conflict("endpoint with duplicate path found");
        }

        var endpoint = await _endpoints.Create(new()
        {
            AgentId = agent.Id.ToString(),
            Path = request.Path,
            Dialect = request.Dialect,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        }, cancellationToken);

        return Results.Created($"/agents/{agent.Name}/endpoints/{endpoint.Path}", endpoint);
    }
}