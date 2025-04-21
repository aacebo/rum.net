using Rum.Graph.Contexts;

namespace Rum.Graph.Resolvers;

public class CustomResolver<T> : IResolver<T> where T : notnull
{
    public string Name => Info.Name;
    public readonly Type Info;

    private readonly object? _object;
    private readonly ObjectResolver _resolver;

    public CustomResolver(T? value = default)
    {
        Info = typeof(T);
        _object = value;
        _resolver = new(Info, value);
    }

    public bool Select(string key)
    {
        return Name == key;
    }

    public async Task<Result<T>> Resolve(IContext context)
    {
        foreach (var (key, query) in context.Query.Fields)
        {
            var res = await _resolver.Resolve(new ObjectContext()
            {
                Services = context.Services,
                Query = query,
                Parent = context.Parent,
                
            });

            if (!res.IsError)
            {

            }
        }

        return Result<T>.Ok((T?)_object);
    }
}