namespace Rum.Cmd.Annotations;

[AttributeUsage(
    AttributeTargets.Class | AttributeTargets.Struct,
    Inherited = true
)]
public class CommandAttribute(string? name = null, string? description = null) : Attribute
{
    public string? Name { get; } = name;
    public string? Description { get; } = description;

    public bool Select(string key) => Name == key;
}