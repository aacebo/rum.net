namespace Rum.Graph.Annotations;

[AttributeUsage(
    AttributeTargets.Class | AttributeTargets.Struct,
    Inherited = true
)]
public class SchemaAttribute(string? name = null) : Attribute
{
    public string? Name { get; } = name;
    public string? Description { get; }
}