using System.Reflection;

using Rum.Graph.Annotations;
using Rum.Graph.Contexts;

namespace Rum.Graph.Resolvers;

public class MemberResolver : IResolver
{
    public string Name => _member.Name;
    public readonly MemberInfo Info;

    private readonly object? _object;
    private readonly IResolver _member;

    public MemberResolver(MemberInfo member, object? value = null)
    {
        Info = member;
        _object = value;

        if (member is FieldInfo field)
        {
            _member = new FieldResolver(field, value);
        }
        else if (member is PropertyInfo property)
        {
            _member = new PropertyResolver(property, value);
        }
        else if (member is MethodInfo method)
        {
            _member = new MethodResolver(method, value);
        }

        if (_member is not null)
        {
            return;
        }

        throw new InvalidOperationException($"member type '{member.MemberType}' is not supported");
    }

    public bool Select(string key)
    {
        return _member.Select(key);
    }

    public async Task<Result> Resolve(IContext context)
    {
        var res = await _member.Resolve(context);

        if (res.Data is not null)
        {
            var schema = Info.GetType().GetCustomAttribute<SchemaAttribute>();

            if (schema is not null)
            {
                var resolver = new ObjectResolver(Info.GetType(), res.Data);

                return await resolver.Resolve(new ObjectContext()
                {
                    Query = context.Query,
                    Parent = _object,
                    Value = res.Data
                });
            }
        }

        return res;
    }
}