using System.Reflection;

namespace Rum.Graph.Annotations;

public class ParamAttribute(string? name = null) : ContextAccessorAttribute
{
    public string? Name { get; } = name;

    public override object? Resolve(IContext<object> context, ParameterInfo parameter)
    {
        var name = Name ?? parameter.Name;
        return name is null ? null : context.Query.Args.Get(name);
    }
}