using Rum.Core;

namespace Rum.Data.Rules.String;

/// <summary>
/// Exact Length
/// </summary>
public class Length(int length) : IRule
{
    public string Name => "string.length";
    public string Message => $"must have length of {length}";

    public IResult<object> Resolve(object? value)
    {
        if (value == null) return Result.Ok();
        return ((string)value).Length == length ? Result.Ok(value) : Result.Err(Message);
    }
}