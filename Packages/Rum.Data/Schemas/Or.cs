using Rum.Core;

namespace Rum.Data;

public static partial class Schemas
{
    /// <summary>
    /// Schema used to validate if at least one
    /// other schema matches the input
    /// </summary>
    public static OrSchema Or(params ISchema[] schemas) => new(schemas);
}

/// <summary>
/// Schema used to validate if at least one
/// other schema matches the input
/// </summary>
public class OrSchema(params ISchema[] schemas) : AnySchema
{
    public override string Name => "or";

    public override OrSchema Message(string message) => (OrSchema)base.Message(message);
    public override OrSchema Rule(IRule rule) => (OrSchema)base.Rule(rule);
    public override OrSchema Rule(string name, Rule.ResolverFn resolve) => (OrSchema)base.Rule(name, resolve);
    public override OrSchema Required() => (OrSchema)base.Required();
    public override OrSchema Default(object defaultValue) => (OrSchema)base.Default(defaultValue);
    public override OrSchema Transform(Func<object?, object?> transform) => (OrSchema)base.Transform(transform);
    public override OrSchema Merge(AnySchema schema) => (OrSchema)base.Merge(schema);
    public override IResult<object> Validate(object? value)
    {
        var res = base.Validate(value);

        if (res.Error != null)
        {
            return res;
        }

        foreach (var schema in schemas)
        {
            var result = schema.Validate(value);

            if (result.Error == null)
            {
                return result;
            }
        }

        return Result.Err($"expected one of: [{string.Join(", ", schemas.Select(r => r.Name))}]");
    }
}