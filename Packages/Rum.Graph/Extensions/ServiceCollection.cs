using Microsoft.Extensions.DependencyInjection;

namespace Rum.Graph.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddResolver<TEntity, TResolver>(this IServiceCollection collection) where TEntity : notnull, new() where TResolver : class, IResolver<TEntity>
    {
        collection.AddSingleton<IResolver<TEntity>, TResolver>();
        return collection;
    }
}