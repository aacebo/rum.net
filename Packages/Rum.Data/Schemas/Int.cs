using Rum.Core;

namespace Rum.Data;

public static partial class Schemas
{
    /// <summary>
    /// Schema used to validate integers
    /// </summary>
    public static IntSchema Int() => new();
}

/// <summary>
/// Schema used to validate integers
/// </summary>
public class IntSchema : AnySchema
{
    public override string Name => "int";

    public IntSchema(AnySchema? schema = null) : base(schema)
    {
        Rule(new Rule("int", value =>
        {
            if (value == null) return Result.Ok();
            if (value is int asInt) return Result.Ok(asInt);
            return Result.Err("must be an integer");
        }));
    }

    public override IntSchema Message(string message) => (IntSchema)base.Message(message);
    public override IntSchema Rule(IRule rule) => (IntSchema)base.Rule(rule);
    public override IntSchema Rule(string name, Rule.ResolverFn resolve) => (IntSchema)base.Rule(name, resolve);
    public override IntSchema Required() => (IntSchema)base.Required();
    public override IntSchema Merge(AnySchema schema) => (IntSchema)base.Merge(schema);

    public IntSchema Enum(params int[] options) => Rule(new Rules.Enum<int>(options));
    public IntSchema Default(int defaultValue) => (IntSchema)base.Default(defaultValue);
    public IntSchema Transform(Func<int?, int?> transform) => (IntSchema)base.Transform(transform);
    public IntSchema Min(int min) => Rule(new Rules.Int.Min(min));
    public IntSchema Max(int max) => Rule(new Rules.Int.Max(max));
    public IntSchema MultipleOf(int multipleOf) => Rule(new Rules.Int.MultipleOf(multipleOf));
    public IntSchema Positive() => Rule(new Rules.Int.Min(0));
    public IntSchema Negative() => Rule(new Rules.Int.Max(-1));
    public IntSchema Even() => Rule(new Rules.Int.Even());
    public IntSchema Odd() => Rule(new Rules.Int.Odd());

    public IResult<int?> Validate(int? value) => new Result<int?>(base.Validate(value));
}