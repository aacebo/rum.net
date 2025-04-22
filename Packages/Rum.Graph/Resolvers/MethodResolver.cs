using System.Reflection;

namespace Rum.Graph.Resolvers;

public class MethodResolver : IResolver
{
    private readonly object? _object;
    public readonly MethodInfo _method;
    private readonly ParameterResolver[] _parameters;

    public MethodResolver(MethodInfo method, object? value = null)
    {
        _method = method;
        _object = value;
        _parameters = method.GetParameters().Select(param => new ParameterResolver(param)).ToArray();
    }

    public async Task<Result> Resolve(IContext context)
    {
        try
        {
            var paramters = _parameters.Select(p => p.Resolve(context)).ToArray();
            var res = _method.Invoke(_object, paramters);

            if (res is Task<object?> task)
            {
                res = await task;
            }

            return Result.Ok(res);
        }
        catch (Exception ex)
        {
            return Result.Err(ex.ToString());
        }
    }
}