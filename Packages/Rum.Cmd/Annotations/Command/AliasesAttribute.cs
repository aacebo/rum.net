using System.Reflection;

namespace Rum.Cmd.Annotations.Command;

public class AliasesAttribute(params string[] aliases) : CommandBuilderAttribute
{
    public IList<string> Aliases { get; } = aliases;

    public override bool Select(string key) => Aliases.Any(a => a == key);
    public override Rum.Cmd.Command.Builder Apply(Rum.Cmd.Command.Builder builder, PropertyInfo property)
    {
        builder = builder.Aliases([.. Aliases]);
        return builder;
    }
}