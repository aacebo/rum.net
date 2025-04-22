using System.Reflection;

using Rum.Graph.Annotations;
using Rum.Graph.Contexts;

namespace Rum.Graph.Resolvers;

internal class ParameterResolver
{
    private readonly ContextAccessorAttribute? _accessor;

    public ParameterResolver(ParameterInfo parameter)
    {
        _accessor = parameter.GetCustomAttribute<ContextAccessorAttribute>();
    }

    public object? Resolve(IContext context)
    {
        var param = (ParamContext)context;
        return _accessor is null ? context : _accessor.GetValue(param);
    }
}