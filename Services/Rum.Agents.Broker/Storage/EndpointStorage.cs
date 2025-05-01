using SqlKata.Execution;

namespace Rum.Agents.Broker.Storage;

public interface IEndpointStorage
{
    public Task<IEnumerable<Models.Endpoint>> GetByAgentId(Guid agentId, CancellationToken cancellationToken = default);
    public Task<Models.Endpoint> Create(Models.Endpoint endpoint, CancellationToken cancellationToken = default);
    public Task Delete(Guid agentId, string path, CancellationToken cancellationToken = default);
}

public class EndpointStorage(QueryFactory db) : IEndpointStorage
{
    public async Task<IEnumerable<Models.Endpoint>> GetByAgentId(Guid agentId, CancellationToken cancellationToken = default)
    {
        return await db.Query()
            .Select("*")
            .From("endpoints")
            .Where("agent_id", "=", agentId)
            .OrderByDesc("created_at")
            .GetAsync<Models.Endpoint>(cancellationToken: cancellationToken);
    }

    public async Task<Models.Endpoint> Create(Models.Endpoint endpoint, CancellationToken cancellationToken = default)
    {
        await db.Query("endpoints").InsertAsync(endpoint, cancellationToken: cancellationToken);
        return endpoint;
    }

    public async Task Delete(Guid agentId, string path, CancellationToken cancellationToken = default)
    {
        await db.Query("endpoints")
            .Where("agent_id", "=", agentId)
            .Where("path", "=", path)
            .DeleteAsync(cancellationToken: cancellationToken);
    }
}