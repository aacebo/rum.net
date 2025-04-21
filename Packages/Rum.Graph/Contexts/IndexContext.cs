namespace Rum.Graph.Contexts;

public class IndexContext<T> : Dictionary<string, object?>, IContext<T>
{
    public required Query Query { get; set; }
    public T? Parent { get; set; }
    public required int Index { get; set; }
}