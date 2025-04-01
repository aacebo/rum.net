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
public class BoolSchema : AnySchema<bool?>, ISchema<bool?>
{
    public override string Name => "bool";

    public BoolSchema() : base()
    {
        Rule(new Rule("bool", value =>
        {
            if (value == null) return Result<object?>.Ok();
            if (value is bool asBool) return Result<object?>.Ok(asBool);
            return Result<object?>.Err("must be a bool");
        }));
    }

    public override BoolSchema Message(string message) => (BoolSchema)base.Message(message);
    public override BoolSchema Rule(IRule rule) => (BoolSchema)base.Rule(rule);
    public override BoolSchema Rule(string name, Rule.ResolverFn resolve) => (BoolSchema)base.Rule(name, resolve);
    public override BoolSchema Required() => (BoolSchema)base.Required();
    public override BoolSchema Enum(params bool?[] options) => (BoolSchema)base.Enum(options);
    public override BoolSchema Default(bool? defaultValue) => (BoolSchema)base.Default(defaultValue);
    public override BoolSchema Transform(Func<bool?, bool?> transform) => (BoolSchema)base.Transform(transform);
}