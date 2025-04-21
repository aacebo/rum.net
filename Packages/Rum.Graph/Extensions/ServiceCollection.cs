using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

using Rum.Graph.Annotations;
using Rum.Graph.Resolvers;

namespace Rum.Graph.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddResolver<TResolves, TResolver>(this IServiceCollection collection) where TResolves : class where TResolver : class
    {
        var attribute = typeof(T).GetCustomAttribute<ResolverBaseAttribute>();

        if (attribute is null)
        {
            throw new InvalidOperationException();
        }

        var resolver = new ObjectResolver();

        collection.AddSingleton<IResolver<TResolves>>(attribute.Type);
        return collection;
    }
}