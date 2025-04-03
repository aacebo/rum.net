using Rum.Core;

namespace Rum.Data.Rules.Double;

/// <summary>
/// Max Value
/// </summary>
public class Max(double max) : IRule
{
    public string Name => "double.max";
    public string Message => $"must have value of at most {max}";

    public IResult<object> Resolve(object? value)
    {
        if (value == null) return Result.Ok();
        return ((double)value) <= max ? Result.Ok(value) : Result.Err(Message);
    }
}