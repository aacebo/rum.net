using Rum.Graph.Contexts;
using Rum.Graph.Extensions;

namespace Rum.Graph.Resolvers;

internal class ListResolver(IResolver resolver, IResolver indexResolver) : IResolver
{
    public string Name => resolver.Name;
    public Type EntityType => typeof(IList<>).MakeGenericType(indexResolver.EntityType);

    public async Task<Result> Resolve(IContext context)
    {
        var listResult = await resolver.Resolve(context);

        if (listResult.IsError)
        {
            return listResult;
        }

        var enumerable = (IEnumerable<object>?)listResult.Data ?? [];
        var type = enumerable.GetType();
        var itemType = type.GetIndexType();
        var listType = typeof(IList<>).MakeGenericType(itemType);
        var result = Result.Ok(enumerable);

        for (var i = 0; i < enumerable.Count(); i++)
        {
            var res = await indexResolver.Resolve(new Context()
            {
                Query = context.Query,
                Value = enumerable.ElementAt(i)
            });

            result.Meta.Merge(res.Meta);

            if (res.Error is not null)
            {
                result.Error ??= new();
                result.Error.Add(new Error()
                {
                    Key = i.ToString(),
                    Errors = [res.Error]
                });

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

    public Schema ToSchema()
    {
        return new($"List<{indexResolver.Name}>");
    }
}