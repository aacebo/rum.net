using Rum.Core;

namespace Rum.Data;

public static partial class Schemas
{
    /// <summary>
    /// Schema used to validate booleans
    /// </summary>
    public static BoolSchema Bool() => new();
}

/// <summary>
/// Schema used to validate booleans
/// </summary>
public class BoolSchema : AnySchema
{
    public override string Name => "bool";

    public BoolSchema(AnySchema? schema = null) : base(schema)
    {
        Rule(new Rule("bool", value =>
        {
            if (value == null) return Result.Ok();
            if (value is bool asBool) return Result.Ok(asBool);
            return Result.Err("must be a bool");
        }));
    }

    public override BoolSchema Message(string message) => (BoolSchema)base.Message(message);
    public override BoolSchema Rule(IRule rule) => (BoolSchema)base.Rule(rule);
    public override BoolSchema Rule(string name, Rule.ResolverFn resolve) => (BoolSchema)base.Rule(name, resolve);
    public override BoolSchema Required() => (BoolSchema)base.Required();
    public override BoolSchema Merge(AnySchema schema) => (BoolSchema)base.Merge(schema);

    public BoolSchema Enum(params bool[] options) => Rule(new Rules.Enum<bool>(options));
    public BoolSchema Default(bool defaultValue) => (BoolSchema)base.Default(defaultValue);
    public BoolSchema Transform(Func<bool?, bool?> transform) => (BoolSchema)base.Transform(transform);

    public IResult<bool?> Validate(bool? value) => new Result<bool?>(base.Validate(value));
}