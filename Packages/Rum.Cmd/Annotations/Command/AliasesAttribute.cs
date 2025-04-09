namespace Rum.Cmd.Annotations.Command;

[AttributeUsage(
    AttributeTargets.Class | AttributeTargets.Struct,
    Inherited = true
)]
public class AliasesAttribute(params string[] aliases) : Attribute
{
    public IList<string> Aliases { get; } = aliases;

    public bool Select(string key) => Aliases.Any(a => a == key);
}