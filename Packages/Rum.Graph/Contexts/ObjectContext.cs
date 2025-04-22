namespace Rum.Graph.Contexts;

public class Context<T> : Dictionary<string, object?>, IContext<T>
{
    public required Query Query { get; set; }
    public T? Parent { get; set; }
}