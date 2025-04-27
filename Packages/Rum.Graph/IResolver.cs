namespace Rum.Graph;

public interface IResolver
{
    public string Name { get; }

    public Task<Result> Resolve(IContext context);
}