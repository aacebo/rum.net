namespace Rum.Graph;

public interface IResolver : IResolver<object>;
public interface IResolver<T>
{
    public string Name { get; }

    public Task<Result> Resolve(IContext<T> context);
}