namespace Rum.Graph.Contexts;

public class Context : Dictionary<string, object?>, IContext
{
    public required Query Query { get; set; }
    public object? Parent { get; set; }
}