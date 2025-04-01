using Rum.Core;

namespace Rum.Data;

public static partial class Schemas
{
    /// <summary>
    /// Any Schema
    /// </summary>
    public static AnySchema Any() => new();
}

/// <summary>
/// Any Schema
/// </summary>
public class AnySchema : AnySchema<object?>, ISchema<object?>;

/// <summary>
/// Any Schema
/// </summary>
public class AnySchema<T> : ISchema<T>
{
    public virtual string Name => "any";

    protected List<IRule> Rules { get; set; } = [];
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

    public virtual AnySchema<T> Transform(Func<T, T> transform)
    {
        return Rule(new Rules.Transform<T>(transform));
    }

    public virtual IResult<T> Validate(T value)
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

            if (res.Value is T parsed)
            {
                current = parsed;
            }
        }

        return errors.Empty ? Result<T>.Ok(current) : Result<T>.Err(errors);
    }

    public IResult<object?> Resolve(object? value)
    {
        if (value is not T)
        {
            return Result<object?>.Err($"type '{value?.GetType().Name ?? "null"}' is not compatible with '{typeof(T).Name}'");
        }

        var res = Validate((T)value);

        if (res.Error != null)
        {
            return _message != null ? Result<object?>.Err(_message) : Result<object?>.Err(res.Error);
        }

        return Result<object?>.Ok(res.Value);
    }
}