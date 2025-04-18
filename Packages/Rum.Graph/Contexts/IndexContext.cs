namespace Rum.Graph.Contexts;

public class IndexContext : Dictionary<string, object?>, IContext
{
    public required Query Query { get; set; }
    public object? Parent { get; set; }
    public required int Index { get; set; }
    public object? Value { get; set; }
}