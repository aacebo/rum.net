using Rum.Core;

namespace Rum.Data.Rules.Int;

/// <summary>
/// Max Value
/// </summary>
public class Max(int max) : IRule
{
    public string Name => "int.max";
    public string Message => $"must have value of at most {max}";

    public IResult<object?> Resolve(object? value)
    {
        if (value == null) return Result<object?>.Ok();
        return ((int)value) <= max ? Result<object?>.Ok(value) : Result<object?>.Err(Message);
    }
}