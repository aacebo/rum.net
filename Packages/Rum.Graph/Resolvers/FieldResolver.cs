using System.Reflection;

using Rum.Graph.Annotations;
using Rum.Graph.Contexts;

namespace Rum.Graph.Resolvers;

internal class FieldResolver : IResolver
{
    public string Name { get; }

    private readonly object? _object;
    private readonly MethodInfo _method;
    private readonly ParameterResolver[] _parameters;

    public FieldResolver(MethodInfo method, object? value = null)
    {
        Name = method.GetCustomAttribute<FieldAttribute>()?.Name ?? method.Name;
        _method = method;
        _object = value;
        _parameters = method.GetParameters().Select(param => new ParameterResolver(param)).ToArray();
    }

    public async Task<Result> Resolve(IContext context)
    {
        var fieldContext = (FieldContext)context;

        try
        {
            List<object?> parameters = [];
            var errors = new Error() { Key = "args" };

            foreach (var param in _parameters)
            {
                var result = param.Resolve(new ParamContext()
                {
                    Query = fieldContext.Query,
                    Value = fieldContext.Value,
                    Key = param.Name,
                    Parameter = param.Parameter
                });

                if (result.Error is not null)
                {
                    errors.Add(result.Error);
                    continue;
                }

                parameters.Add(result.Data);
            }

            if (errors.Count > 0)
            {
                return Result.Err(errors);
            }

            var res = _method.Invoke(_object, parameters.ToArray());

            if (res is Task<object?> task)
            {
                res = await task;
            }

            return Result.Ok(res);
        }
        catch (Exception ex)
        {
            return Result.Err(Name, ex.ToString());
        }
    }
}