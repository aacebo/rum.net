namespace Rum.Cmd.Annotations.Option;

[AttributeUsage(
    AttributeTargets.Property | AttributeTargets.Field,
    Inherited = true
)]
public class AliasesAttribute(params string[] aliases) : Attribute
{
    public IList<string> Aliases { get; } = aliases;

    public bool Select(string key) => Aliases.Any(a => a == key);
}