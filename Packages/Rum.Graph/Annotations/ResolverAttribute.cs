namespace Rum.Graph.Annotations;

[AttributeUsage(
    AttributeTargets.Class | AttributeTargets.Struct,
    Inherited = true
)]
public abstract class ResolverBaseAttribute(string? name = null) : Attribute
{
    public abstract Type Type { get; }
    public string? Name { get; } = name;
    public string? Description { get; }
}

public class ResolverAttribute<T>(string? name = null) : ResolverBaseAttribute(name ?? typeof(T).Name)
{
    public override Type Type { get; } = typeof(T);
}

public class ResolverAttribute(Type type, string? name = null) : ResolverBaseAttribute(name ?? type.Name)
{
    public override Type Type { get; } = type;
}