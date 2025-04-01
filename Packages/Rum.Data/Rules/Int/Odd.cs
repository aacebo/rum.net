using Rum.Core;

namespace Rum.Data.Rules.Int;

/// <summary>
/// Odd Value
/// </summary>
public class Odd : IRule
{
    public string Name => "int.odd";
    public string Message => "must be odd";

    public IResult<object?> Resolve(object? value)
    {
        if (value == null) return Result<object?>.Ok();
        return ((int)value) % 2 != 0 ? Result<object?>.Ok(value) : Result<object?>.Err(Message);
    }
}