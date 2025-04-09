namespace Rum.Cmd.Annotations;

[AttributeUsage(
    AttributeTargets.Property | AttributeTargets.Field,
    Inherited = true
)]
public class PositionalAttribute(int index = 0, string? description = null) : Attribute
{
    public int Index { get; } = index;
    public string? Description { get; } = description;

    public bool Select(string key) => Index.ToString() == key;
    public bool Select(int index) => Index == index;
}