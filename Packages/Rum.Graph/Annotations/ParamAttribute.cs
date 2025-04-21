using System.Reflection;

namespace Rum.Graph.Annotations;

public class ParamAttribute : ContextAccessorAttribute
{
    public override object? Resolve(IContext<object> context, ParameterInfo parameter)
    {
        return parameter.Name is null ? null : context.Query.Args.Get(parameter.Name);
    }
}