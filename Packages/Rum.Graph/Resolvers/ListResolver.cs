using Microsoft.Extensions.DependencyInjection;

using Rum.Graph.Contexts;
using Rum.Graph.Exceptions;

namespace Rum.Graph.Resolvers;

public class ListResolver : IResolver<object[]>
{
    public string Name => typeof(object[]).Name;

    private readonly IServiceProvider _services;

    public ListResolver(IServiceProvider services)
    {
        _services = services;
    }

    public async Task<Result> Resolve(IContext<object[]> context)
    {
        var arr = context.Parent ?? [];
        var type = arr.GetType().GetElementType() ?? throw new InvalidTypeException(arr.GetType());
        var resolver = (IResolver<object>)_services.GetRequiredService(type);
        var result = Result.Ok(arr);

        for (var i = 0; i < arr.Length; i++)
        {
            var res = await resolver.Resolve(new IndexContext<object>()
            {
                Query = context.Query,
                Parent = arr,
                Index = i
            });

            if (res.Error is not null)
            {
                result.Error ??= new();
                result.Error.Add(res.Error);
                continue;
            }

            arr.SetValue(res.Data, i);
        }

        return result;
    }
}