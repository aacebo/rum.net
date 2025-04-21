namespace Rum.Graph.Contexts;

public class FieldContext<T> : Dictionary<string, object?>, IContext
{
    public required Query Query { get; set; }
    public object? Parent { get; set; }
    public required string Key { get; set; }
}