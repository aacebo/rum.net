namespace Rum.Graph.Contexts;

public class FieldContext : Dictionary<string, object?>, IContext
{
    public required Query Query { get; set; }
    public object? Parent { get; set; }
    public required string Key { get; set; }
    public object? Value { get; set; }
}