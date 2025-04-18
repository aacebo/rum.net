using System.Reflection;

namespace Rum.Graph.Annotations;

[AttributeUsage(
    AttributeTargets.Parameter,
    Inherited = true
)]
public abstract class ContextAccessorAttribute : Attribute
{
    public abstract object? Resolve(IContext context, ParameterInfo parameter);
}