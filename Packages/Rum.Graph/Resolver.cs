using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

using Rum.Graph.Annotations;
using Rum.Graph.Contexts;
using Rum.Graph.Exceptions;
using Rum.Graph.Parsing;
using Rum.Graph.Resolvers;

namespace Rum.Graph;

public class Resolver<T> : IResolver where T : notnull, new()
{
    private readonly MethodResolver[] _methods;
    private readonly MemberInfo[] _members;
    private readonly IServiceProvider _services;
    private readonly ListResolver _list;

    public Resolver(IServiceProvider services)
    {
        _services = services;
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
        var value = context.Parent ?? new T();
        var result = Result.Ok(value);

        foreach (var member in _members)
        {
            if (!context.Query.Fields.Exists(member.GetName()))
            {
                member.SetValue(value, null);
            }
        }

        foreach (var (key, query) in context.Query.Fields)
        {
            var member = _members.Where(member => member.GetName() == key).FirstOrDefault();

            if (member is null)
            {
                result.Error ??= new();
                result.Error.Add(key, "field not found");
                continue;
            }

            var method = _methods.Where(m =>
            {
                var name = m._method.GetCustomAttribute<FieldAttribute>()?.Name ?? m._method.Name;
                return name == key;
            }).FirstOrDefault();

            if (method is null)
            {
                result.Error ??= new();
                result.Error.Add(key, "field not found");
                continue;
            }

            var res = await method.Resolve(new FieldContext()
            {
                Query = query,
                Parent = value,
                Key = key,
                Member = member
            });

            result.Meta.Merge(res.Meta);

            if (res.IsError && res.Error is not null)
            {
                result.Error ??= new();
                result.Error.Add(new Error()
                {
                    Key = key,
                    Errors = [res.Error]
                });

                continue;
            }

            if (res.Data is IEnumerable<object> list)
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
                    var resolver = (IResolver)_services.GetRequiredService(attribute.Type);
                    res = await resolver.Resolve(new Context()
                    {
                        Query = query,
                        Parent = res.Data
                    });
                }
            }

            member.SetValue(value, res.Data);
        }

        return result;
    }
}