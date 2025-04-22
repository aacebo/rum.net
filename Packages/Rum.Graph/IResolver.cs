namespace Rum.Graph;

public interface IResolver
{
    public Task<Result> Resolve(IContext context);
}