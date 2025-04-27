namespace Rum.Graph.Resolvers;

internal class StaticResolver : IResolver
{
    public string Name { get; }

    private readonly object? _value;

    public StaticResolver(object? value)
    {
        Name = "static";
        _value = value;
    }

    public Task<Result> Resolve(IContext _)
    {
        return Task.FromResult(Result.Ok(_value));
    }
}