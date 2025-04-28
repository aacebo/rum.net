using Rum.Graph.Contexts;

namespace Rum.Graph.Resolvers;

internal class ListResolver(IResolver resolver) : IResolver
{
    public string Name => $"List[{resolver.Name}]";

    public async Task<Result> Resolve(IContext context)
    {
        var enumerable = (IEnumerable<object>?)context.Value ?? [];
        var type = enumerable.GetType();
        var itemType = GetEnumerableType(type);
        var listType = typeof(IList<>).MakeGenericType(itemType);
        var result = Result.Ok(enumerable);

        for (var i = 0; i < enumerable.Count(); i++)
        {
            var res = await resolver.Resolve(new Context()
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
        return new()
        {
            Type = Name
        };
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