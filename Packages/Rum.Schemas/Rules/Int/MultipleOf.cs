using Rum.Core;

namespace Rum.Schemas.Rules.Int;

/// <summary>
/// Multiple Of Value
/// </summary>
public class MultipleOf(int multipleOf) : IRule
{
    public string Name => "int.multipleOf";
    public string Message => $"must have value that is multiple of {multipleOf}";

    public IResult<object?> Resolve(object? value)
    {
        if (value == null) return Result<object?>.Ok();
        return (multipleOf % ((int)value)) == 0 ? Result<object?>.Ok(value) : Result<object?>.Err(Message);
    }
}