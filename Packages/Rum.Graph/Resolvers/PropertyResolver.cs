using System.Reflection;

using Rum.Graph.Annotations;

namespace Rum.Graph.Resolvers;

public class PropertyResolver : IResolver<object>
{
    public string Name => _attribute.Name ?? Info.Name;
    public readonly PropertyInfo Info;

    private readonly object? _object;
    private readonly FieldAttribute _attribute;

    public PropertyResolver(PropertyInfo property, object? value = null)
    {
        Info = property;
        _object = value;
        _attribute = property.GetCustomAttribute<FieldAttribute>() ?? throw new InvalidOperationException();
    }

    public bool Select(string key)
    {
        return Name == key;
    }

    public Task<Result<object>> Resolve(IContext _)
    {
        return Task.FromResult(Result<object>.Ok(Info.GetValue(_object)));
    }
}