using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

using Rum.Graph.Annotations;
using Rum.Graph.Contexts;
using Rum.Graph.Exceptions;
using Rum.Graph.Parsing;
using Rum.Graph.Resolvers;

namespace Rum.Graph;

public class Resolver<T> : IResolver where T : notnull
{
    public string Name => typeof(T).Name;

    private readonly FieldResolver[] _fields;
    private readonly MemberInfo[] _members;
    private readonly IServiceProvider _services;
    private readonly ListResolver _list;

    public Resolver(IServiceProvider services)
    {
        _services = services;
        _list = new ListResolver(_services);
        _fields = GetType()
            .GetMethods()
            .Where(method => method.GetCustomAttribute<FieldAttribute>() is not null)
            .Select(method => new FieldResolver(method, this))
            .ToArray();

        _members = typeof(T)
            .GetMembers()
            .Where(member => member is PropertyInfo || member is FieldInfo)
            .ToArray();
    }

    public async Task<Result> Resolve(T value, string qs)
    {
        var query = new Parser(qs).Parse();
        return await Resolve(new Context()
        {
            Query = query,
            Value = value
        });
    }

    public async Task<Result> Resolve(IContext context)
    {
        var result = Result.Ok(context.Value);

        foreach (var (key, query) in context.Query.Fields)
        {
            var member = _members.Where(member => member.GetName() == key).FirstOrDefault();

            if (member is null)
            {
                result.Error ??= new();
                result.Error.Add(key, "field not found");
                continue;
            }

            IResolver? field = _fields.Where(m => m.Name == key).FirstOrDefault();

            if (field is null)
            {
                field = new StaticResolver(member.GetValue(result.Data));
            }

            var res = await field.Resolve(new FieldContext()
            {
                Query = query,
                Value = context.Value,
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
                    Value = list
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
                        Value = res.Data
                    });
                }
            }

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

            member.SetValue(result.Data, res.Data);
        }

        return result;
    }
}