using Rum.Core;

namespace Rum.Data.Rules.Array;

/// <summary>
/// Max Length
/// </summary>
public class Max(int max) : IRule
{
    public string Name => "array.max";
    public string Message => $"must have length of at most {max}";

    public IResult<object> Resolve(object? value)
    {
        if (value == null) return Result.Ok();
        return ((IEnumerable<object?>)value).Count() <= max ? Result.Ok(value) : Result.Err(Message);
    }
}