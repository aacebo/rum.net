namespace Rum.Graph.Annotations;

[AttributeUsage(AttributeTargets.Method, Inherited = true)]
public class FieldAttribute(string? name = null) : Attribute
{
    public string? Name { get; } = name;
    public string? Description { get; }
}