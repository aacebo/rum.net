using System.Reflection;

using Rum.Graph.Annotations;

namespace Rum.Graph.Resolvers;

public class MethodResolver : IResolver
{
    public string Name => _attribute.Name ?? Info.Name;
    public readonly MethodInfo Info;

    private readonly object? _object;
    private readonly ParameterResolver[] _parameters;
    private readonly FieldAttribute _attribute;

    public MethodResolver(MethodInfo method, object? value = null)
    {
        Info = method;
        _object = value;
        _parameters = method.GetParameters().Select(param => new ParameterResolver(param)).ToArray();
        _attribute = method.GetCustomAttribute<FieldAttribute>() ?? throw new InvalidOperationException($"method '{method.Name}' isn't a Schema.Field");
    }

    public bool Select(string key)
    {
        return Name == key;
    }

    public async Task<Result> Resolve(IContext context)
    {
        try
        {
            var paramters = _parameters.Select(p => p.Resolve(context)).ToArray();
            var res = Info.Invoke(_object, paramters);

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