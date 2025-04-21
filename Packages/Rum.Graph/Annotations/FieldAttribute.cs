namespace Rum.Graph.Annotations;

[AttributeUsage(
    AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method,
    Inherited = true
)]
public class FieldAttribute(string? name = null) : Attribute
{
    public string? Name { get; } = name;
    public string? Description { get; }
}