using System.Reflection;

namespace Rum.Cmd.Annotations;

[AttributeUsage(
    AttributeTargets.Property | AttributeTargets.Field,
    Inherited = true
)]
public abstract class CommandBuilderAttribute : Attribute
{
    public abstract bool Select(string key);
    public abstract Rum.Cmd.Command.Builder Apply(Rum.Cmd.Command.Builder builder, PropertyInfo property);
}