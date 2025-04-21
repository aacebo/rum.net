namespace Rum.Graph.Contexts;

public class ObjectContext : Dictionary<string, object?>, IContext
{
    public required IServiceProvider Services { get; set; }
    public required Query Query { get; set; }
    public object? Parent { get; set; }
    public object? Value { get; set; }
}