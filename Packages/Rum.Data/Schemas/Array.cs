using Rum.Core;

namespace Rum.Data;

public static partial class Schemas
{
    /// <summary>
    /// Schema used to validate arrays
    /// </summary>
    public static ArraySchema Array(params ISchema[] items) => new(items);
}

/// <summary>
/// Schema used to validate arrays
/// </summary>
public class ArraySchema : AnySchema
{
    public override string Name => "array";
    public readonly ISchema[] Items;

    public ArraySchema(params ISchema[] items) : base()
    {
        Items = items;
        Rule(new Rule("array", value =>
        {
            if (value == null) return Result.Ok();
            if (value is object?[] asArray) return Result.Ok(asArray);
            if (value is IEnumerable<object?> asEnum) return Result.Ok(asEnum.ToArray());
            return Result.Err("must be an array");
        }));
    }

    public override ArraySchema Message(string message) => (ArraySchema)base.Message(message);
    public override ArraySchema Rule(IRule rule) => (ArraySchema)base.Rule(rule);
    public override ArraySchema Rule(string name, Rule.ResolverFn resolve) => (ArraySchema)base.Rule(name, resolve);
    public override ArraySchema Required() => (ArraySchema)base.Required();
    public override ArraySchema Merge(AnySchema schema) => (ArraySchema)base.Merge(schema);

    public ArraySchema Transform(Func<object[]?, object[]?> transform) => (ArraySchema)base.Transform(transform);
    public ArraySchema Min(int minLength) => Rule(new Rules.Array.Min(minLength));
    public ArraySchema Max(int maxLength) => Rule(new Rules.Array.Max(maxLength));

    public override IResult<object> Validate(object? value)
    {
        var res = base.Validate(value);

        if (res.Error != null)
        {
            return res;
        }

        var errors = new ErrorGroup(_message);
        var list = ((object[]?)res.Value) ?? [];

        for (var i = 0; i < list.Length; i++)
        {
            var schema = Items.Length == 1 ? Items.First() : i < Items.Length ? Items[i] : null;

            if (schema == null)
            {
                continue;
            }

            var result = schema.Validate(list[i]);

            if (result.Error != null)
            {
                errors.Add(Errors.Index(i, result.Error));
            }

            if (result.Value != null)
            {
                list[i] = result.Value;
            }
        }

        return errors.Empty ? Result.Ok(list) : Result.Err(errors);
    }
}