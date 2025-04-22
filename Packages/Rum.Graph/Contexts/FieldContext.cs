namespace Rum.Graph.Contexts;

public class FieldContext<T> : Context<T>
{
    public required string Key { get; set; }
}