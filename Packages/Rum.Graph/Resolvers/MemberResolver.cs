
using System.Reflection;

using Rum.Graph.Exceptions;

namespace Rum.Graph.Resolvers;

internal class MemberResolver : IResolver
{
    public string Name => _member.GetName();

    private readonly MemberInfo _member;

    public MemberResolver(MemberInfo member)
    {
        _member = member;
    }

    public Task<Result> Resolve(IContext context)
    {
        return Task.FromResult(Result.Ok(_member.GetValue(context.Value)));
    }
}