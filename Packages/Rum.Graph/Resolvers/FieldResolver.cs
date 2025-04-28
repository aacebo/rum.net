using System.Reflection;

using Rum.Graph.Annotations;

namespace Rum.Graph.Resolvers;

internal class FieldResolver(IResolver resolver) : IResolver
{
    public string Name => Attribute.Name;

    public required MemberInfo Member { get; set; }
    public required MethodInfo Method { get; set; }
    public required FieldAttribute Attribute { get; set; }

    public Task<Result> Resolve(IContext context)
    {
        return Attribute.Resolve(resolver, Method, context);
    }

    public Schema ToSchema()
    {
        return new()
        {
            Type = Member is PropertyInfo property
                ? property.PropertyType.Name
                : Member is FieldInfo field
                    ? field.FieldType.Name
                    : Name,
        };
    }
}