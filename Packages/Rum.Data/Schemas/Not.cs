using Rum.Core;

namespace Rum.Data;

public static partial class Schemas
{
    /// <summary>
    /// Schema used to validate if no other
    //  schema matches the input
    /// </summary>
    public static NotSchema Not(params ISchema[] schemas) => new(schemas);
}

/// <summary>
/// Schema used to validate if no other
//  schema matches the input
/// </summary>
public class NotSchema(params ISchema[] schemas) : AnySchema
{
    public override string Name => "or";

    public override NotSchema Message(string message) => (NotSchema)base.Message(message);
    public override NotSchema Rule(IRule rule) => (NotSchema)base.Rule(rule);
    public override NotSchema Rule(string name, Rule.ResolverFn resolve) => (NotSchema)base.Rule(name, resolve);
    public override NotSchema Required() => (NotSchema)base.Required();
    public override NotSchema Default(object defaultValue) => (NotSchema)base.Default(defaultValue);
    public override NotSchema Transform(Func<object?, object?> transform) => (NotSchema)base.Transform(transform);
    public override NotSchema Merge(AnySchema schema) => (NotSchema)base.Merge(schema);
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
                return Result.Err($"expected not to be: [{string.Join(", ", schemas.Select(r => r.Name))}]");
            }
        }

        return Result.Ok(value);
    }
}