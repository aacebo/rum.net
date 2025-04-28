
using System.Reflection;

using Rum.Graph.Exceptions;

namespace Rum.Graph.Resolvers;

internal class MemberResolver(MemberInfo member) : IResolver
{
    public string Name => member.GetName();

    public Task<Result> Resolve(IContext context)
    {
        return Task.FromResult(Result.Ok(member.GetValue(context.Value)));
    }

    public Schema ToSchema()
    {
        return new()
        {
            Type = member is PropertyInfo property
                ? property.PropertyType.Name
                : member is FieldInfo field
                    ? field.FieldType.Name
                    : Name,
        };
    }
}