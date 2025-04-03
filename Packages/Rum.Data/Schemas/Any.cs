using Rum.Core;

namespace Rum.Data;

public static partial class Schemas
{
    /// <summary>
    /// Any Schema
    /// </summary>
    public static AnySchema Any(AnySchema? schema = null) => new(schema);
}

/// <summary>
/// Any Schema
/// </summary>
public class AnySchema : ISchema
{
    public virtual string Name => "any";

    protected HashSet<IRule> Rules { get; set; } = [];
    protected string? _message;

    public AnySchema(AnySchema? schema = null)
    {
        _message = schema?._message;

        if (schema != null)
        {
            Merge(schema);
        }
    }

    public virtual AnySchema Rule(IRule rule)
    {
        Rules.Add(rule);
        return this;
    }

    public virtual AnySchema Message(string message)
    {
        _message = message;
        return this;
    }

    public virtual AnySchema Rule(string name, Rule.ResolverFn resolve)
    {
        return Rule(new Rule(name, resolve));
    }

    public virtual AnySchema Required()
    {
        return Rule(new Rules.Required());
    }

    public virtual AnySchema Enum(params object[] options)
    {
        return Rule(new Rules.Enum<object>(options));
    }

    public virtual AnySchema Default(object defaultValue)
    {
        return Rule(new Rules.Default<object>(defaultValue));
    }

    public virtual AnySchema Transform(Func<object?, object?> transform)
    {
        return Rule(new Rules.Transform<object>(transform));
    }

    public virtual AnySchema Transform<T>(Func<T?, T?> transform)
    {
        return Rule(new Rules.Transform<T>(transform));
    }

    public virtual AnySchema Merge(AnySchema schema)
    {
        foreach (var rule in schema.Rules)
        {
            Rules.Add(rule);
        }

        return this;
    }

    public virtual IResult<object> Validate(object? value)
    {
        var errors = new ErrorGroup(_message);
        var current = value;

        foreach (var rule in Rules)
        {
            var res = rule.Resolve(current);

            if (res.Error != null)
            {
                errors.Add(Errors.Rule(rule.Name, res.Error.GetError()));
            }

            current = res.Value;
        }

        return errors.Empty ? Result.Ok(current) : Result.Err(errors);
    }
}