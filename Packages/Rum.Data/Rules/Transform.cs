using Rum.Core;

namespace Rum.Data.Rules;

/// <summary>
/// transform an input value into an output
/// </summary>
public class Transform<T>(Func<T?, T?> transform) : IRule
{
    public string Name => $"transform[{typeof(T).Name}]";

    public IResult<object?> Resolve(object? value)
    {
        var output = transform((T?)value);
        return Result<object?>.Ok(output);
    }
}