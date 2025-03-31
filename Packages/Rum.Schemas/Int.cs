using Rum.Core;

namespace Rum.Schemas;

/// <summary>
/// Schema used to validate integers
/// </summary>
public class Int : Any<int?>, ISchema<int?>
{
    public override string Name => "int";

    public Int() : base()
    {
        Rule(new Rule("int", value =>
        {
            if (value == null) return Result<object?>.Ok();
            if (value is int asInt) return Result<object?>.Ok(asInt);
            return Result<object?>.Err("must be an integer");
        }));
    }

    public override Int Rule(IRule rule) => (Int)base.Rule(rule);
    public override Int Rule(string name, Rule.ResolverFn resolve) => (Int)base.Rule(name, resolve);
    public override Int Required() => (Int)base.Required();
    public override Int Enum(params int?[] options) => (Int)base.Enum(options);
    public override Int Default(int? defaultValue) => (Int)base.Default(defaultValue);
    public override Int Not(params IRule[] rules) => (Int)base.Not(rules);

    public Int Min(int min) => Rule(new Rules.Int.Min(min));
    public Int Max(int max) => Rule(new Rules.Int.Max(max));
    public Int MultipleOf(int multipleOf) => Rule(new Rules.Int.MultipleOf(multipleOf));
    public Int Positive() => Rule(new Rules.Int.Min(0));
    public Int Negative() => Rule(new Rules.Int.Max(-1));
    public Int Even() => Rule(new Rules.Int.Even());
    public Int Odd() => Rule(new Rules.Not(new Rules.Int.Even()));
}