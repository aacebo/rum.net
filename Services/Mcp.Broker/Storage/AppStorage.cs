using Mcp.Broker.Models;

using Npgsql;

namespace Mcp.Broker.Storage;

public interface IAppStorage
{
    public Task<App> GetById(Guid id);
    public Task<App> Create(App app);
    public Task<App> Update(App app);
    public Task Delete(Guid id);
}

public class AppStorage(NpgsqlDataSource database) : IAppStorage
{
    public Task<App> GetById(Guid id)
    {
        database.CreateCommand("SELECT * FROM apps");
    }

    public Task<App> Create(App app)
    {
        throw new NotImplementedException();
    }

    public Task<App> Update(App app)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}