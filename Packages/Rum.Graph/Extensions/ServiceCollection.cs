using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

using Rum.Graph.Annotations;
using Rum.Graph.Resolvers;

namespace Rum.Graph.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddResolver<T>(this IServiceCollection collection) where T : class
    {
        var attribute = typeof(T).GetCustomAttribute<ResolverBaseAttribute>();

        if (attribute is null)
        {
            throw new InvalidOperationException();
        }

        collection.AddSingleton(attribute.Type);
        return collection;
    }
}