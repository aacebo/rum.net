using System.Reflection;

using Rum.Cmd.Options;

namespace Rum.Cmd.Annotations.Option;

public class AliasesAttribute(params string[] aliases) : OptionBuilderAttribute
{
    public IList<string> Aliases { get; } = aliases;

    public override bool Select(string key) => Aliases.Any(a => a == key);
    public override Named.Builder Apply(Named.Builder builder, PropertyInfo property)
    {
        builder.Alias([.. Aliases]);
        return builder;
    }

    public override Positional.Builder Apply(Positional.Builder builder, PropertyInfo property)
    {
        throw new InvalidOperationException("positional options cannot have aliases");
    }
}