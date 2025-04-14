using System.Reflection;

using Rum.Data;

namespace Rum.Cmd.Annotations;

public class OptionAttribute(string? name = null, string? description = null) : CommandBuilderAttribute
{
    public string? Name { get; } = name;
    public string? Description { get; } = description;

    public override bool Select(string key) => Name == key;
    public override Rum.Cmd.Command.Builder Apply(Rum.Cmd.Command.Builder builder, PropertyInfo property)
    {
        var option = new Options.Named.Builder().Name(Name ?? property.Name).Type(Schemas.Get(property.PropertyType));

        if (Description != null)
        {
            option = option.Description(Description);
        }

        var attributes = property.GetCustomAttributes<OptionBuilderAttribute>();

        foreach (var attr in attributes)
        {
            option = attr.Apply(option, property);
        }

        builder = builder.Option(option.Build());
        return builder;
    }
}