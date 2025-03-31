using Rum.Core;

namespace Rum.Schemas;

/// <summary>
/// Any Schema
/// </summary>
public class Any : Any<object>, ISchema<object?>;

/// <summary>
/// Any Schema
/// </summary>
public class Any<T> : ISchema<T?>
{
    public virtual string Name => "any";

    protected List<IRule> Rules { get; set; } = [];

    public virtual Any<T> Rule(IRule rule)
    {
        Rules.Add(rule);
        return this;
    }

    public virtual Any<T> Rule(string name, Rule.ResolverFn resolve)
    {
        return Rule(new Rule(name, resolve));
    }

    public virtual Any<T> Required()
    {
        return Rule(new Rules.Required());
    }

    public virtual Any<T> Enum(params T[] options)
    {
        return Rule(new Rules.Enum<T>(options));
    }

    public virtual Any<T> Default(T defaultValue)
    {
        return Rule(new Rules.Default<T>(defaultValue));
    }

    public virtual Any<T> Not(params IRule[] rules)
    {
        return Rule(new Rules.Not(rules));
    }

    public virtual IResult<T?> Validate(object? value)
    {
        var errors = new ErrorGroup();
        var current = value;

        foreach (var rule in Rules)
        {
            var res = rule.Resolve(current);

            if (res.Error != null)
            {
                errors.Add(Errors.Rule(rule.Name, res.Error.Message));
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
            return Result<object?>.Err(res.Error);
        }

        return Result<object?>.Ok(res.Value);
    }
}