using System.Reflection;

using Rum.Graph.Annotations;

namespace Rum.Graph.Resolvers;

public class FieldResolver : IResolver
{
    public string Name => _attribute.Name ?? Info.Name;
    public readonly FieldInfo Info;

    private readonly object? _object;
    private readonly Schema.FieldAttribute _attribute;

    public FieldResolver(FieldInfo field, object? value = null)
    {
        Info = field;
        _object = value;
        _attribute = field.GetCustomAttribute<Schema.FieldAttribute>() ?? throw new InvalidOperationException();
    }

    public bool Select(string key)
    {
        return Name == key;
    }

    public Task<Result> Resolve(IContext _)
    {
        return Task.FromResult(Result.Ok(Info.GetValue(_object)));
    }
}