namespace Rum.Graph;

public interface IResolver<T> where T : notnull
{
    public string Name { get; }

    public bool Select(string key);
    public Task<Result<T>> Resolve(IContext context);
}
