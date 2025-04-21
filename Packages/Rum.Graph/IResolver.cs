namespace Rum.Graph;

public interface IResolver
{
    public string Name { get; }

    public bool Select(string key);
    public Task<Result> Resolve(IContext context);
}
