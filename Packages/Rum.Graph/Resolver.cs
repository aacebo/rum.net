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

    private readonly Type _type;
    private readonly IResolver[] _fields;
    private readonly IServiceProvider _services;

    public Resolver(IServiceProvider services)
    {
        _services = services;
        _type = typeof(T);
        _fields = _type
            .GetMembers()
            .Where(member => member is PropertyInfo || member is FieldInfo)
            .Select<MemberInfo, IResolver>(member =>
            {
                var method = GetType()
                    .GetMethods()
                    .Where(method => method.GetCustomAttribute<FieldAttribute>()?.Name == member.GetName())
                    .FirstOrDefault();

                if (method is null)
                {
                    return new MemberResolver(member);
                }

                return new FieldResolver(this)
                {
                    Member = member,
                    Method = method,
                    Attribute = method.GetCustomAttribute<FieldAttribute>()!
                };
            })
            .ToArray();
    }

    public async Task<Result> Resolve(T value, string qs)
    {
        return await Resolve(new Context()
        {
            Query = new Parser(qs).Parse(),
            Value = value
        });
    }

    public async Task<Result> Resolve(IContext context)
    {
        var result = Result.Ok(context.Value);

        foreach (var (key, query) in context.Query.Fields)
        {
            var field = _fields.Where(f => f.Name == key).FirstOrDefault();

            if (field is null)
            {
                result.Error ??= new();
                result.Error.Add(key, "not found");
                continue;
            }

            var res = await field.Resolve(new Context()
            {
                Query = query,
                Value = context.Value
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
                var itemType = ListResolver.GetEnumerableType(list.GetType());
                var resolverType = itemType.GetCustomAttribute<ResolverBaseAttribute>()?.Type;

                if (resolverType is not null)
                {
                    var resolver = (IResolver)_services.GetRequiredService(resolverType);
                    var listResolver = new ListResolver(resolver);

                    res = await listResolver.Resolve(new Context()
                    {
                        Query = query,
                        Value = list
                    });
                }
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

            if (field is FieldResolver fieldResolver)
            {
                fieldResolver.Member.SetValue(result.Data, res.Data);
            }
        }

        return result;
    }

    public Schema ToSchema()
    {
        return new()
        {
            Type = _type.Name,
            Fields = _fields.ToDictionary(
                field => field.Name,
                field => field.ToSchema()
            )
        };
    }
}