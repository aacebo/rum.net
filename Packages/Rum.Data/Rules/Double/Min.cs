using Rum.Core;

namespace Rum.Data.Rules.Double;

/// <summary>
/// Min Value
/// </summary>
public class Min(double min) : IRule
{
    public string Name => "double.min";
    public string Message => $"must have value of at least {min}";

    public IResult<object> Resolve(object? value)
    {
        if (value == null) return Result.Ok();
        return ((double)value) >= min ? Result.Ok(value) : Result.Err(Message);
    }
}