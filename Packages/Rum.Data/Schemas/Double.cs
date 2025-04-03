using Rum.Core;

namespace Rum.Data;

public static partial class Schemas
{
    /// <summary>
    /// Schema used to validate doubles
    /// </summary>
    public static DoubleSchema Double() => new();
}

/// <summary>
/// Schema used to validate doubles
/// </summary>
public class DoubleSchema : AnySchema
{
    public override string Name => "double";

    public DoubleSchema() : base()
    {
        Rule(new Rule("double", value =>
        {
            if (value == null) return Result.Ok();
            if (value is int asInt) return Result.Ok((double)asInt);
            if (value is float asFloat) return Result.Ok((double)asFloat);
            if (value is decimal asDecimal) return Result.Ok((double)asDecimal);
            if (value is double asDouble) return Result.Ok(asDouble);
            return Result.Err("must be a double");
        }));
    }

    public override DoubleSchema Message(string message) => (DoubleSchema)base.Message(message);
    public override DoubleSchema Rule(IRule rule) => (DoubleSchema)base.Rule(rule);
    public override DoubleSchema Rule(string name, Rule.ResolverFn resolve) => (DoubleSchema)base.Rule(name, resolve);
    public override DoubleSchema Required() => (DoubleSchema)base.Required();
    public override DoubleSchema Merge(AnySchema schema) => (DoubleSchema)base.Merge(schema);

    public DoubleSchema Enum(params double[] options) => (DoubleSchema)base.Enum(options);
    public DoubleSchema Default(double defaultValue) => (DoubleSchema)base.Default(defaultValue);
    public DoubleSchema Transform(Func<double?, double?> transform) => (DoubleSchema)base.Transform(transform);
    public DoubleSchema Min(double min) => Rule(new Rules.Double.Min(min));
    public DoubleSchema Max(double max) => Rule(new Rules.Double.Max(max));
    public DoubleSchema Positive() => Rule(new Rules.Double.Min(0));
    public DoubleSchema Negative() => Rule(new Rules.Double.Max(-1));
}