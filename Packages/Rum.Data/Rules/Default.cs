using Rum.Core;

namespace Rum.Data.Rules;

/// <summary>
/// when null, use a default value
/// </summary>
public class Default<T>(T defaultValue) : IRule
{
    public string Name => "default";

    public IResult<object?> Resolve(object? value)
    {
        return Result<object?>.Ok(value ?? defaultValue);
    }
}