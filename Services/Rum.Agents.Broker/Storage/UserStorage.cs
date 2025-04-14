using Rum.Agents.Broker.Models;

using SqlKata.Execution;

namespace Rum.Agents.Broker.Storage;

public interface IUserStorage
{
    public Task<IEnumerable<User>> Get(CancellationToken cancellationToken = default);
    public Task<User?> GetById(Guid id, CancellationToken cancellationToken = default);
    public Task<User> Create(User user, CancellationToken cancellationToken = default);
    public Task<User> Update(User user, CancellationToken cancellationToken = default);
    public Task Delete(Guid id, CancellationToken cancellationToken = default);
}

public class UserStorage(QueryFactory db) : IUserStorage
{
    public async Task<IEnumerable<User>> Get(CancellationToken cancellationToken = default)
    {
        var users = await db.Query()
            .Select("*")
            .From("users")
            .OrderByDesc("created_at")
            .GetAsync<User>(cancellationToken: cancellationToken);

        return users;
    }

    public async Task<User?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        var users = await db.Query()
            .Select("*")
            .From("users")
            .Where("id", "=", id)
            .GetAsync<User>(cancellationToken: cancellationToken);

        return users.FirstOrDefault();
    }

    public async Task<User> Create(User user, CancellationToken cancellationToken = default)
    {
        await db.Query("users").InsertAsync(user, cancellationToken: cancellationToken);
        return user;
    }

    public async Task<User> Update(User user, CancellationToken cancellationToken = default)
    {
        await db.Query("users")
            .Where("id", "=", user.Id)
            .UpdateAsync(user, cancellationToken: cancellationToken);

        return user;
    }

    public async Task Delete(Guid id, CancellationToken cancellationToken = default)
    {
        await db.Query("users")
            .Where("id", "=", id)
            .DeleteAsync(cancellationToken: cancellationToken);
    }
}