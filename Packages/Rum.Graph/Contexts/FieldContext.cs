namespace Rum.Graph.Contexts;

public class FieldContext<T> : Dictionary<string, object?>, IContext<T>
{
    public required Query Query { get; set; }
    public T? Parent { get; set; }
    public required string Key { get; set; }
}