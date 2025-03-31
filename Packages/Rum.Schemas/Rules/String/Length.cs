using Rum.Core;

namespace Rum.Schemas.Rules.String;

public class Length(int length) : IRule
{
    public string Name => "string.length";
    public string Message => $"must have length of {length}";

    public IResult<object?> Resolve(object? value)
    {
        if (value == null) return Result<object?>.Ok();
        return ((string)value).Length == length ? Result<object?>.Ok(value) : Result<object?>.Err(Message);
    }
}