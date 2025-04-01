using Rum.Core;

namespace Rum.Data.Rules.Double;

/// <summary>
/// Max Value
/// </summary>
public class Max(double max) : IRule
{
    public string Name => "double.max";
    public string Message => $"must have value of at most {max}";

    public IResult<object?> Resolve(object? value)
    {
        if (value == null) return Result<object?>.Ok();
        return ((double)value) <= max ? Result<object?>.Ok(value) : Result<object?>.Err(Message);
    }
}