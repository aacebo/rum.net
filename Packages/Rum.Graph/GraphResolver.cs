using Rum.Graph.Contexts;
using Rum.Graph.Parsing;
using Rum.Graph.Resolvers;

namespace Rum.Graph;

public static class GraphResolver
{
    public static async Task<Result> Resolve<T>(string qs) where T : new()
    {
        var value = new T();
        var query = new Parser(qs).Parse();
        var resolver = new ObjectResolver(typeof(T), value);

        return await resolver.Resolve(new ObjectContext()
        {
            Query = query,
            Value = value
        });
    }

    public static async Task<Result> Resolve<T>(byte[] qs) where T : new()
    {
        var value = new T();
        var query = new Parser(qs).Parse();
        var resolver = new ObjectResolver(typeof(T), value);

        return await resolver.Resolve(new ObjectContext()
        {
            Query = query,
            Value = value
        });
    }

    public static async Task<Result> Resolve<T>(char[] qs) where T : new()
    {
        var value = new T();
        var query = new Parser(qs).Parse();
        var resolver = new ObjectResolver(typeof(T), value);

        return await resolver.Resolve(new ObjectContext()
        {
            Query = query,
            Value = value
        });
    }

    public static async Task<Result> Resolve<T>(string qs, T value)
    {
        var query = new Parser(qs).Parse();
        var resolver = new ObjectResolver(typeof(T), value);

        return await resolver.Resolve(new ObjectContext()
        {
            Query = query,
            Value = value
        });
    }

    public static async Task<Result> Resolve<T>(byte[] qs, T value)
    {
        var query = new Parser(qs).Parse();
        var resolver = new ObjectResolver(typeof(T), value);

        return await resolver.Resolve(new ObjectContext()
        {
            Query = query,
            Value = value
        });
    }

    public static async Task<Result> Resolve<T>(char[] qs, T value)
    {
        var query = new Parser(qs).Parse();
        var resolver = new ObjectResolver(typeof(T), value);

        return await resolver.Resolve(new ObjectContext()
        {
            Query = query,
            Value = value
        });
    }
}