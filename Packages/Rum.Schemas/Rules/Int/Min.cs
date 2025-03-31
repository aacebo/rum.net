using Rum.Core;

namespace Rum.Schemas.Rules.Int;

/// <summary>
/// Min Value
/// </summary>
public class Min(int min) : IRule
{
    public string Name => "int.min";
    public string Message => $"must have value of at least {min}";

    public IResult<object?> Resolve(object? value)
    {
        if (value == null) return Result<object?>.Ok();
        return ((int)value) >= min ? Result<object?>.Ok(value) : Result<object?>.Err(Message);
    }
}