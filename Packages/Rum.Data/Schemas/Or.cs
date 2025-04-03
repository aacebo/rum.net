using Rum.Core;

namespace Rum.Data;

public static partial class Schemas
{
    /// <summary>
    /// Schema used to validate if at least one
    /// other schema matches the input
    /// </summary>
    public static OrSchema Or(params IRule[] rules) => new(rules);
}

/// <summary>
/// Schema used to validate if at least one
/// other schema matches the input
/// </summary>
public class OrSchema(params IRule[] rules) : AnySchema<object?>(), ISchema<object?>
{
    public override string Name => "or";

    public override OrSchema Message(string message) => (OrSchema)base.Message(message);
    public override OrSchema Rule(IRule rule) => (OrSchema)base.Rule(rule);
    public override OrSchema Rule(string name, Rule.ResolverFn resolve) => (OrSchema)base.Rule(name, resolve);
    public override OrSchema Required() => (OrSchema)base.Required();
    public override OrSchema Default(object? defaultValue) => (OrSchema)base.Default(defaultValue);
    public override OrSchema Transform(Func<object?, object?> transform) => (OrSchema)base.Transform(transform);
    public override OrSchema Merge<R>(AnySchema<R> schema) => (OrSchema)base.Merge(schema);
    public override IResult<object?> Validate(object? value)
    {
        var res = base.Validate(value);

        if (res.Error != null)
        {
            return res;
        }

        foreach (var rule in rules)
        {
            var result = rule.Resolve(value);

            if (result.Error == null)
            {
                return result;
            }
        }

        return Result<object?>.Err($"expected one of: [{string.Join(", ", rules.Select(r => r.Name))}]");
    }
}