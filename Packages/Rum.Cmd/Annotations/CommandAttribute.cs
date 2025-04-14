using System.Reflection;

namespace Rum.Cmd.Annotations;

[AttributeUsage(
    AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property | AttributeTargets.Field,
    Inherited = true
)]
public class CommandAttribute(string? name = null, string? description = null) : CommandBuilderAttribute
{
    public string? Name { get; } = name;
    public string? Description { get; } = description;

    public override  bool Select(string key) => Name == key;
    public override Rum.Cmd.Command.Builder Apply(Rum.Cmd.Command.Builder builder, PropertyInfo property)
    {
        var cmd = Rum.Cmd.Command.From(property.PropertyType);

        if (Description != null)
        {
            cmd = cmd.Description(Description);
        }

       builder = builder.Command(cmd.Build());
       return builder;
    }
}