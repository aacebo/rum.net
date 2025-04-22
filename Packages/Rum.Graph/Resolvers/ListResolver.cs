using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

using Rum.Graph.Annotations;
using Rum.Graph.Contexts;

namespace Rum.Graph.Resolvers;

public class ListResolver : IResolver
{
    private readonly IServiceProvider _services;

    public ListResolver(IServiceProvider services)
    {
        _services = services;
    }

    public async Task<Result> Resolve(IContext context)
    {
        IResolver resolver = new ObjectResolver<object>(_services);

        var enumerable = (IEnumerable<object>?)context.Parent ?? [];
        var type = enumerable.GetType();
        var itemType = GetEnumerableType(enumerable.GetType());
        var listType = typeof(IList<>).MakeGenericType(itemType);
        var attribute = itemType.GetCustomAttribute<ResolverBaseAttribute>();
        var result = Result.Ok(enumerable);

        if (attribute is not null)
        {
            resolver = (IResolver)_services.GetRequiredService(attribute.Type);
        }

        for (var i = 0; i < enumerable.Count(); i++)
        {
            var res = await resolver.Resolve(new IndexContext()
            {
                Query = context.Query,
                Parent = enumerable.ElementAt(i),
                Index = i
            });

            if (res.Error is not null)
            {
                result.Error ??= new();
                result.Error.Add(res.Error);
                continue;
            }

            if (res.Data is not null)
            {
                if (type.IsArray)
                {
                    var method = type.GetMethod("SetValue", [typeof(object), typeof(int)]);
                    method?.Invoke(enumerable, [res.Data, i]);
                }
                else if (type.IsAssignableTo(listType))
                {
                    var property = type.GetProperty("Item");
                    property?.SetValue(enumerable, res.Data, [i]);
                }
            }
        }

        return result;
    }

    public static Type GetEnumerableType(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            return type.GetGenericArguments()[0];

        var iface = (from i in type.GetInterfaces()
                     where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>)
                     select i).FirstOrDefault();

        if (iface == null)
            throw new ArgumentException("Does not represent an enumerable type.", nameof(type));

        return GetEnumerableType(iface);
    }
}