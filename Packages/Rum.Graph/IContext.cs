namespace Rum.Graph;

public interface IContext : IContext<object>;
public interface IContext<T> : IDictionary<string, object?>
{
    public Query Query { get; set; }
    public T? Parent { get; set; }
}