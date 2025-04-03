using Rum.Core;

namespace Rum.Data;

public static partial class Schemas
{
    /// <summary>
    /// Any Schema
    /// </summary>
    public static AnySchema<object?> Any() => new();

    /// <summary>
    /// Any Schema
    /// </summary>
    public static AnySchema<T> Any<T>() => new();
}

/// <summary>
/// Any Schema
/// </summary>
public class AnySchema : AnySchema<object?>, ISchema<object?>;

/// <summary>
/// Any Schema
/// </summary>
public class AnySchema<T> : ISchema<T?>
{
    public virtual string Name => "any";

    protected HashSet<IRule> Rules { get; set; } = [];
    protected string? _message;

    public virtual AnySchema<T> Rule(IRule rule)
    {
        Rules.Add(rule);
        return this;
    }

    public virtual AnySchema<T> Message(string message)
    {
        _message = message;
        return this;
    }

    public virtual AnySchema<T> Rule(string name, Rule.ResolverFn resolve)
    {
        return Rule(new Rule(name, resolve));
    }

    public virtual AnySchema<T> Required()
    {
        return Rule(new Rules.Required());
    }

    public virtual AnySchema<T> Enum(params T[] options)
    {
        return Rule(new Rules.Enum<T>(options));
    }

    public virtual AnySchema<T> Default(T defaultValue)
    {
        return Rule(new Rules.Default<T>(defaultValue));
    }

    public virtual AnySchema<T> Transform(Func<T?, T?> transform)
    {
        return Rule(new Rules.Transform<T>(transform));
    }

    public virtual AnySchema<T> Merge<R>(AnySchema<R> schema)
    {
        foreach (var rule in schema.Rules)
        {
            Rules.Add(rule);
        }

        return this;
    }

    public virtual AnySchema ToAny()
    {
        return (AnySchema)new AnySchema().Merge(this);
    }

    public virtual AnySchema<R> ToAny<R>()
    {
        return new AnySchema<R>().Merge(this);
    }

    public virtual IResult<T?> Validate(object? value)
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

        return errors.Empty ? Result<T?>.Ok((T?)current) : Result<T?>.Err(errors);
    }

    public IResult<object?> Resolve(object? value)
    {
        var res = Validate(value);

        if (res.Error != null)
        {
            return _message != null ? Result<object?>.Err(_message) : Result<object?>.Err(res.Error);
        }

        return Result<object?>.Ok(res.Value);
    }

    public static implicit operator AnySchema<T>(AnySchema schema) => schema;
    public static implicit operator AnySchema<T>(StringSchema schema) => schema;
    public static implicit operator AnySchema<T>(AndSchema schema) => schema;
    public static implicit operator AnySchema<T>(ArraySchema<object?> schema) => schema;
    public static implicit operator AnySchema<T>(BoolSchema schema) => schema;
    public static implicit operator AnySchema<T>(DoubleSchema schema) => schema;
    public static implicit operator AnySchema<T>(IntSchema schema) => schema;
    public static implicit operator AnySchema<T>(NotSchema schema) => schema;
    public static implicit operator AnySchema<T>(ObjectSchema schema) => schema;
    public static implicit operator AnySchema<T>(OrSchema schema) => schema;
}