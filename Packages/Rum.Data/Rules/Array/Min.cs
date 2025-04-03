using Rum.Core;

namespace Rum.Data.Rules.Array;

/// <summary>
/// Min Length
/// </summary>
public class Min(int min) : IRule
{
    public string Name => "array.min";
    public string Message => $"must have length of at least {min}";

    public IResult<object> Resolve(object? value)
    {
        if (value == null) return Result.Ok();
        return ((IEnumerable<object?>)value).Count() >= min ? Result.Ok(value) : Result.Err(Message);
    }
}