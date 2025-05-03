using System.Reflection;

using Rum.Graph.Contexts;
using Rum.Graph.Exceptions;

namespace Rum.Graph.Resolvers;

internal class MemberResolver : IResolver
{
    public string Name => Member.GetName();
    public Type EntityType => Member.GetPropertyOrFieldType();

    public required MemberInfo Member { get; set; }
    public IResolver? Resolver { get; set; }

    public async Task<Result> Resolve(IContext context)
    {
        var value = Member.GetValue(context.Value);

        if (Resolver is null)
        {
            return Result.Ok(value);
        }

        var res = await Resolver.Resolve(new Context()
        {
            Query = context.Query,
            Value = context.Value
        });

        return res;
    }

    public Schema ToSchema()
    {
        return Resolver?.ToSchema() ?? new(Member);
    }
}