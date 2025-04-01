using Rum.Core;

namespace Rum.Data;

public static partial class Schemas
{
    /// <summary>
    /// Schema used to validate arrays
    /// </summary>
    public static ArraySchema<T> Array<T>(params IRule[] items) => new(items);

    /// <summary>
    /// Schema used to validate arrays
    /// </summary>
    public static ArraySchema<object?> Array(params IRule[] items) => new(items);
}

/// <summary>
/// Schema used to validate arrays
/// </summary>
public class ArraySchema<T> : AnySchema<T[]?>, ISchema<T[]?>
{
    public override string Name => "array";
    public readonly IRule[] Items;

    public ArraySchema(params IRule[] items) : base()
    {
        Items = items;
        Rule(new Rule("array", value =>
        {
            if (value == null) return Result<object?>.Ok();
            if (value is T[] asArray) return Result<object?>.Ok(asArray);
            if (value is IEnumerable<T> asEnum) return Result<object?>.Ok(asEnum.ToArray());
            return Result<object?>.Err("must be an array");
        }));
    }

    public override ArraySchema<T> Rule(IRule rule) => (ArraySchema<T>)base.Rule(rule);
    public override ArraySchema<T> Rule(string name, Rule.ResolverFn resolve) => (ArraySchema<T>)base.Rule(name, resolve);
    public override ArraySchema<T> Required() => (ArraySchema<T>)base.Required();
    public override ArraySchema<T> Not(params IRule[] rules) => (ArraySchema<T>)base.Not(rules);

    public ArraySchema<T> Min(int minLength) => Rule(new Rules.Array.Min(minLength));
    public ArraySchema<T> Max(int maxLength) => Rule(new Rules.Array.Max(maxLength));

    public override IResult<T[]?> Validate(object? value)
    {
        var res = base.Validate(value);

        if (res.Error != null)
        {
            return res;
        }

        var errors = new ErrorGroup();
        var list = res.Value ?? [];

        for (var i = 0; i < list.Length; i++)
        {
            var rule = Items.Length == 1 ? Items.First() : i < Items.Length ? Items[i] : null;

            if (rule == null)
            {
                continue;
            }

            var result = new Result<T>(rule.Resolve(list[i]));

            if (result.Error != null)
            {
                errors.Add(Errors.Index(i, result.Error));
            }

            if (result.Value != null)
            {
                list[i] = result.Value;
            }
        }

        return errors.Empty ? Result<T[]?>.Ok(list) : Result<T[]?>.Err(errors);
    }
}