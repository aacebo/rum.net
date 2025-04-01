using Rum.Core;

namespace Rum.Data;

/// <summary>
/// Schema used to validate booleans
/// </summary>
public class Bool : Any<bool?>, ISchema<bool?>
{
    public override string Name => "bool";

    public Bool() : base()
    {
        Rule(new Rule("bool", value =>
        {
            if (value == null) return Result<object?>.Ok();
            if (value is bool asBool) return Result<object?>.Ok(asBool);
            return Result<object?>.Err("must be a bool");
        }));
    }

    public override Bool Rule(IRule rule) => (Bool)base.Rule(rule);
    public override Bool Rule(string name, Rule.ResolverFn resolve) => (Bool)base.Rule(name, resolve);
    public override Bool Required() => (Bool)base.Required();
    public override Bool Enum(params bool?[] options) => (Bool)base.Enum(options);
    public override Bool Default(bool? defaultValue) => (Bool)base.Default(defaultValue);
    public override Bool Not(params IRule[] rules) => (Bool)base.Not(rules);
}