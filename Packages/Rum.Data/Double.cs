using Rum.Core;

namespace Rum.Data;

/// <summary>
/// Schema used to validate doubles
/// </summary>
public class Double : Any<double?>, ISchema<double?>
{
    public override string Name => "double";

    public Double() : base()
    {
        Rule(new Rule("double", value =>
        {
            if (value == null) return Result<object?>.Ok();
            if (value is int asInt) return Result<object?>.Ok((double)asInt);
            if (value is double asDouble) return Result<object?>.Ok(asDouble);
            return Result<object?>.Err("must be a double");
        }));
    }

    public override Double Rule(IRule rule) => (Double)base.Rule(rule);
    public override Double Rule(string name, Rule.ResolverFn resolve) => (Double)base.Rule(name, resolve);
    public override Double Required() => (Double)base.Required();
    public override Double Enum(params double?[] options) => (Double)base.Enum(options);
    public override Double Default(double? defaultValue) => (Double)base.Default(defaultValue);
    public override Double Not(params IRule[] rules) => (Double)base.Not(rules);

    public Double Min(double min) => Rule(new Rules.Double.Min(min));
    public Double Max(double max) => Rule(new Rules.Double.Max(max));
    public Double Positive() => Rule(new Rules.Double.Min(0));
    public Double Negative() => Rule(new Rules.Double.Max(-1));
}