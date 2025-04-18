namespace Rum.Graph;

public interface IContext : IDictionary<string, object?>
{
    public Query Query { get; set; }
    public object? Parent { get; set; }
    public object? Value { get; set; }
}