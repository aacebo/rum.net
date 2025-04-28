using System.Reflection;

namespace Rum.Graph.Annotations;

[AttributeUsage(
    AttributeTargets.Parameter,
    Inherited = true
)]
public abstract class ContextAccessorAttribute : Attribute
{
    public abstract Result Resolve(ParameterInfo param, IContext context);
}