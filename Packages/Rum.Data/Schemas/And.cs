using Rum.Core;

namespace Rum.Data;

public static partial class Schemas
{
    /// <summary>
    /// Schema used to validate if all
    /// schemas matches the input
    /// </summary>
    public static AndSchema And(params ISchema[] schemas) => new(schemas);
}

/// <summary>
/// Schema used to validate if all
/// schemas matches the input
/// </summary>
public class AndSchema(params ISchema[] schemas) : AnySchema()
{
    public override string Name => "and";

    public override AndSchema Message(string message) => (AndSchema)base.Message(message);
    public override AndSchema Rule(IRule rule) => (AndSchema)base.Rule(rule);
    public override AndSchema Rule(string name, Rule.ResolverFn resolve) => (AndSchema)base.Rule(name, resolve);
    public override AndSchema Required() => (AndSchema)base.Required();
    public override AndSchema Default(object defaultValue) => (AndSchema)base.Default(defaultValue);
    public override AndSchema Transform(Func<object?, object?> transform) => (AndSchema)base.Transform(transform);
    public override AndSchema Merge(AnySchema schema) => (AndSchema)base.Merge(schema);
    public override IResult<object> Validate(object? value)
    {
        var res = base.Validate(value);

        if (res.Error != null)
        {
            return res;
        }

        var errors = new ErrorGroup(_message);
        var current = res.Value;

        foreach (var schema in schemas)
        {
            var result = schema.Validate(current);

            if (result.Error != null)
            {
                errors.Add(result.Error);
            }
            else
            {
                current = result.Value;
            }
        }

        return errors.Empty ? Result.Ok(current) : Result.Err(errors);
    }
}