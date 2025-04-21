using System.Reflection;

namespace Rum.Graph.Annotations;

public class ParentAttribute : ContextAccessorAttribute
{
    public override object? Resolve(IContext<object> context, ParameterInfo parameter)
    {
        return context.Parent;
    }
}