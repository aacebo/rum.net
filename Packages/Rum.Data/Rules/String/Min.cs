using Rum.Core;

namespace Rum.Data.Rules.String;

/// <summary>
/// Min Length
/// </summary>
public class Min(int min) : IRule
{
    public string Name => "string.min";
    public string Message => $"must have length of at least {min}";

    public IResult<object> Resolve(object? value)
    {
        if (value == null) return Result.Ok();
        return ((string)value).Length >= min ? Result.Ok(value) : Result.Err(Message);
    }
}