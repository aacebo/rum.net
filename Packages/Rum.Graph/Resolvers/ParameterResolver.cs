using System.Reflection;

using Rum.Graph.Annotations;
using Rum.Graph.Contexts;

namespace Rum.Graph.Resolvers;

internal class ParameterResolver
{
    public ParameterInfo Parameter { get; }

    private readonly ContextAccessorAttribute? _accessor;

    public ParameterResolver(ParameterInfo parameter)
    {
        Parameter = parameter;
        _accessor = parameter.GetCustomAttribute<ContextAccessorAttribute>();
    }

    public Result Resolve(IContext context)
    {
        var param = (ParamContext)context;
        var value = _accessor is null ? context : _accessor.GetValue(param);

        if (value is null)
        {
            if (!param.Parameter.IsOptional)
            {
                return Result.Err(param.Parameter.Position.ToString(), "required");
            }

            return Result.Ok();
        }

        if (!value.GetType().IsAssignableTo(param.Parameter.ParameterType))
        {
            return Result.Err(
                param.Parameter.Position.ToString(),
                $"expected type \"{param.Parameter.ParameterType.Name}\", received \"{value.GetType().Name}\""
            );
        }

        return Result.Ok(value);
    }
}