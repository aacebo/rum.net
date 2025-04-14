using System.Reflection;

namespace Rum.Cmd.Annotations;

[AttributeUsage(
    AttributeTargets.Property | AttributeTargets.Field,
    Inherited = true
)]
public abstract class OptionBuilderAttribute : Attribute
{
    public abstract bool Select(string key);
    public abstract Options.Named.Builder Apply(Options.Named.Builder builder, PropertyInfo property);
    public abstract Options.Positional.Builder Apply(Options.Positional.Builder builder, PropertyInfo property);
}