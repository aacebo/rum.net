using Rum.Core;

namespace Rum.Schemas.Rules.Int;

/// <summary>
/// Even Value
/// </summary>
public class Even : IRule
{
    public string Name => "int.even";
    public string Message => "must be even";

    public IResult<object?> Resolve(object? value)
    {
        if (value == null) return Result<object?>.Ok();
        return ((int)value) % 2 == 0 ? Result<object?>.Ok(value) : Result<object?>.Err(Message);
    }
}