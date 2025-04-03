using Rum.Core;

namespace Rum.Data.Rules.Int;

/// <summary>
/// Odd Value
/// </summary>
public class Odd : IRule
{
    public string Name => "int.odd";
    public string Message => "must be odd";

    public IResult<object> Resolve(object? value)
    {
        if (value == null) return Result.Ok();
        return ((int)value) % 2 != 0 ? Result.Ok(value) : Result.Err(Message);
    }
}