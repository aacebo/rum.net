using System.Reflection;
using System.Text.Json.Serialization;

using Microsoft.Extensions.DependencyInjection;

using Rum.Graph.Annotations;
using Rum.Graph.Contexts;
using Rum.Graph.Parsing;

namespace Rum.Graph.Resolvers;

public class ObjectResolver<T> : IResolver where T : notnull, new()
{
    public string Name => typeof(T).Name;

    private readonly MethodResolver[] _methods;
    private readonly MemberInfo[] _members;
    private readonly IServiceProvider _services;
    private readonly ListResolver _list;

    public ObjectResolver(IServiceProvider services)
    {
        _services = services ?? new ServiceCollection().BuildServiceProvider();
        _list = new ListResolver(_services);
        _methods = GetType()
            .GetMethods()
            .Where(method => method.GetCustomAttribute<FieldAttribute>() is not null)
            .Select(method => new MethodResolver(method, this))
            .ToArray();

        _members = typeof(T)
            .GetMembers()
            .Where(member => member is PropertyInfo || member is FieldInfo)
            .ToArray();
    }

    public async Task<Result> Resolve(string qs, T? value = default)
    {
        var query = new Parser(qs).Parse();
        return await Resolve(new Context()
        {
            Query = query,
            Parent = value
        });
    }

    public async Task<Result> Resolve(IContext context)
    {
        var parent = context.Parent ?? new T();
        var result = Result.Ok(parent);

        foreach (var (key, query) in context.Query.Fields)
        {
            var member = _members.Where(member =>
            {
                var name = member.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name ?? member.Name;
                return name == key;
            }).FirstOrDefault();

            if (member is null)
            {
                result.Error ??= new();
                result.Error.Add($"field '{key}' not found");
                continue;
            }

            var method = _methods.Where(m =>
            {
                var name = m._method.GetCustomAttribute<FieldAttribute>()?.Name ?? m._method.Name;
                return name == key;
            }).FirstOrDefault();

            if (method is null)
            {
                continue;
            }

            var res = await method.Resolve(new FieldContext()
            {
                Query = query,
                Parent = parent,
                Key = key,
                Member = member
            });

            if (res.IsError) continue;
            else if (res.Data is IEnumerable<object> list)
            {
                res = await _list.Resolve(new Context()
                {
                    Query = query,
                    Parent = list
                });
            }
            else if (res.Data is not null)
            {
                var attribute = res.Data.GetType().GetCustomAttribute<ResolverBaseAttribute>();

                if (attribute is not null)
                {
                    var resolver = (IResolver?)_services.GetService(attribute.Type);

                    if (resolver is not null)
                    {
                        res = await resolver.Resolve(new Context()
                        {
                            Query = query,
                            Parent = parent
                        });
                    }
                }
            }

            if (member is FieldInfo field)
            {
                field.SetValue(parent, res.Data);
            }
            else if (member is PropertyInfo property)
            {
                property.SetValue(parent, res.Data);
            }
        }

        return result;
    }
}