using System.Reflection;

using Rum.Graph.Annotations;

namespace Rum.Graph.Resolvers;

public class ParameterResolver
{
    private readonly ParameterInfo _parameter;
    private readonly ContextAccessorAttribute? _accessor;

    public ParameterResolver(ParameterInfo parameter)
    {
        _parameter = parameter;
        _accessor = parameter.GetCustomAttribute<ContextAccessorAttribute>();
    }

    public object? Resolve(IContext context)
    {
        return _accessor is null ? context : _accessor.Resolve(context, _parameter);
    }
}