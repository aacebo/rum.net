using Rum.Agents.Broker.Models;

using SqlKata.Execution;

namespace Rum.Agents.Broker.Storage;

public interface IAgentStorage
{
    public Task<IEnumerable<Agent>> Get(CancellationToken cancellationToken = default);
    public Task<Agent?> GetById(Guid id, CancellationToken cancellationToken = default);
    public Task<Agent?> GetByName(string name, CancellationToken cancellationToken = default);
    public Task<Agent> Create(Agent agent, CancellationToken cancellationToken = default);
    public Task<Agent> Update(Agent agent, CancellationToken cancellationToken = default);
    public Task Delete(Guid id, CancellationToken cancellationToken = default);
}

public class AgentStorage(QueryFactory db) : IAgentStorage
{
    public async Task<IEnumerable<Agent>> Get(CancellationToken cancellationToken = default)
    {
        return await db.Query()
            .Select("*")
            .From("agents")
            .OrderByDesc("created_at")
            .GetAsync<Agent>(cancellationToken: cancellationToken);
    }

    public async Task<Agent?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return await db.Query()
            .Select("*")
            .From("agents")
            .Where("id", "=", id)
            .FirstOrDefaultAsync<Agent>(cancellationToken: cancellationToken);
    }

    public async Task<Agent?> GetByName(string name, CancellationToken cancellationToken = default)
    {
        return await db.Query()
            .Select("*")
            .From("agents")
            .Where("name", "=", name)
            .FirstOrDefaultAsync<Agent>(cancellationToken: cancellationToken);
    }

    public async Task<Agent> Create(Agent agent, CancellationToken cancellationToken = default)
    {
        await db.Query("agents").InsertAsync(agent, cancellationToken: cancellationToken);
        return agent;
    }

    public async Task<Agent> Update(Agent agent, CancellationToken cancellationToken = default)
    {
        await db.Query("agents")
            .Where("id", "=", agent.Id)
            .UpdateAsync(agent, cancellationToken: cancellationToken);

        return agent;
    }

    public async Task Delete(Guid id, CancellationToken cancellationToken = default)
    {
        await db.Query("agents")
            .Where("id", "=", id)
            .DeleteAsync(cancellationToken: cancellationToken);
    }
}