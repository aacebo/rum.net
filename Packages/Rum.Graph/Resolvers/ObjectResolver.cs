using System.Reflection;

using Rum.Graph.Annotations;
using Rum.Graph.Contexts;

namespace Rum.Graph.Resolvers;

public class ObjectResolver : IResolver
{
    public string Name => Info.Name;
    public readonly Type Info;

    private readonly object? _object;
    private readonly MemberResolver[] _members;

    public ObjectResolver(Type type, object? value = null)
    {
        Info = type;
        _object = value;
        _members = type.GetMembers()
            .Where(member => member.GetCustomAttribute<Schema.FieldAttribute>() is not null)
            .Select(member => new MemberResolver(member, value))
            .ToArray();
    }

    public bool Select(string key)
    {
        return Name == key;
    }

    public async Task<Result> Resolve(IContext context)
    {
        foreach (var (key, query) in context.Query.Fields)
        {
            var member = _members.Where(member => member.Select(key)).FirstOrDefault();

            if (member is null)
            {
                throw new MissingMemberException();
            }

            await member.Resolve(new FieldContext()
            {
                Query = query,
                Parent = context.Parent,
                Key = key
            });
        }

        return Result.Ok(_object);
    }
}