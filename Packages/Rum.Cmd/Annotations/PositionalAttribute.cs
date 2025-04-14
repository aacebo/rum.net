using System.Reflection;

using Rum.Data;

namespace Rum.Cmd.Annotations;

public class PositionalAttribute(int index = 0, string? description = null) : CommandBuilderAttribute
{
    public int Index { get; } = index;
    public string? Description { get; } = description;

    public override bool Select(string key) => Index.ToString() == key;
    public bool Select(int index) => Index == index;
    public override Rum.Cmd.Command.Builder Apply(Rum.Cmd.Command.Builder builder, PropertyInfo property)
    {
        var option = new Options.Positional.Builder().Index(Index).Type(Schemas.Get(property.PropertyType));

        if (Description != null)
        {
            option = option.Description(Description);
        }

        var attributes = property.GetCustomAttributes<OptionBuilderAttribute>();

        foreach (var attr in attributes)
        {
            option = attr.Apply(option, property);
        }

        builder = builder.Positional(option.Build());
        return builder;
    }
}