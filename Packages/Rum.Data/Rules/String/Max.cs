using Rum.Core;

namespace Rum.Data.Rules.String;

/// <summary>
/// Max Length
/// </summary>
public class Max(int max) : IRule
{
    public string Name => "string.max";
    public string Message => $"must have length of at most {max}";

    public IResult<object?> Resolve(object? value)
    {
        if (value == null) return Result<object?>.Ok();
        return ((string)value).Length <= max ? Result<object?>.Ok(value) : Result<object?>.Err(Message);
    }
}