using Rum.Agents.Broker.Storage;
using Rum.Graph;
using Rum.Graph.Annotations;

namespace Rum.Agents.Broker.Resolvers;

public class AgentResolver : Resolver<Models.Agent>
{
    private readonly IEndpointStorage _storage;

    public AgentResolver(IServiceProvider provider) : base(provider)
    {
        _storage = provider.GetRequiredService<IEndpointStorage>();
    }

    [Field("endpoints")]
    public async Task<IList<Models.Endpoint>> GetEndpoints([Parent] Models.Agent agent)
    {
        var res = await _storage.GetByAgentId(agent.Id);
        return res.ToList();
    }
}