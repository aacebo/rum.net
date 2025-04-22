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
        IResolver resolver = new ObjectResolver<object>();

        var arr = (IEnumerable<object>?)context.Parent ?? [];
        var attribute = GetEnumerableType(arr.GetType()).GetCustomAttribute<ResolverBaseAttribute>();
        var result = Result.Ok(arr);
        var i = 0;

        if (attribute is not null)
        {
            resolver = (IResolver)_services.GetRequiredService(attribute.Type);
        }

        foreach (var item in arr)
        {
            var res = await resolver.Resolve(new IndexContext()
            {
                Query = context.Query,
                Parent = item,
                Index = i
            });

            if (res.Error is not null)
            {
                result.Error ??= new();
                result.Error.Add(res.Error);
                continue;
            }

            i++;
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