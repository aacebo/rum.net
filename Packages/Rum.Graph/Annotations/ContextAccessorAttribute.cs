using Rum.Graph.Contexts;

namespace Rum.Graph.Annotations;

[AttributeUsage(
    AttributeTargets.Parameter,
    Inherited = true
)]
public abstract class ContextAccessorAttribute : Attribute
{
    public abstract object? GetValue(ParamContext context);
}