namespace Rum.Graph;

public interface ISchema
{
    public string Name { get; }

    public Task<object?> Resolve(IContext context);
}