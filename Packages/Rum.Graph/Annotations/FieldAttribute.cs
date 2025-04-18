namespace Rum.Graph.Annotations;

public static partial class Schema
{
    [AttributeUsage(
        AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method,
        Inherited = true
    )]
    public class FieldAttribute(string? name = null) : Attribute
    {
        public string? Name { get; } = name;
        public string? Description { get; }
    }
}