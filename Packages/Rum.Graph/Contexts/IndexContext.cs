namespace Rum.Graph.Contexts;

public class IndexContext<T> : Context<T>
{
    public required int Index { get; set; }
}