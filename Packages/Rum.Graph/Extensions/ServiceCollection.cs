using Microsoft.Extensions.DependencyInjection;

namespace Rum.Graph.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddResolver<TResolver>(this IServiceCollection collection) where TResolver : class, IResolver
    {
        collection.AddSingleton<TResolver>();
        return collection;
    }
}