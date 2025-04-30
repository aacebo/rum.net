using System.Reflection;

using Rum.Graph.Annotations;
using Rum.Graph.Exceptions;

namespace Rum.Graph.Resolvers;

internal class FieldResolver(IResolver resolver) : IResolver
{
    public string Name => Attribute.Name;
    public Type EntityType => Method.ReturnType;

    public required MemberInfo Member { get; set; }
    public required MethodInfo Method { get; set; }
    public required FieldAttribute Attribute { get; set; }

    public async Task<Result> Resolve(IContext context)
    {
        var res = await Attribute.Resolve(resolver, Method, context);

        if (res.IsError) return res;

        Member.SetValue(context.Value, res.Data);
        return res;
    }

    public Schema ToSchema()
    {
        return new(Member);
    }
}