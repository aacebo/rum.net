using Rum.Core;

namespace Rum.Data.Rules;

/// <summary>
/// transform an input value into an output
/// </summary>
public class Transform<T>(Func<T, T> transform) : IRule
{
    public string Name => $"transform[{typeof(T).Name}]";

    public IResult<object?> Resolve(object? value)
    {
        if (value == null) return Result<object?>.Ok(transform((T)value!));
        if (value is not T) return Result<object?>.Err($"type '{value?.GetType().Name ?? "null"}' is not compatible with '{typeof(T).Name}'");
        var output = transform((T)value);
        return Result<object?>.Ok(output);
    }
}