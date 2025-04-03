using Rum.Core;

namespace Rum.Data.Rules.Int;

/// <summary>
/// Multiple Of Value
/// </summary>
public class MultipleOf(int multipleOf) : IRule
{
    public string Name => "int.multipleOf";
    public string Message => $"must have value that is multiple of {multipleOf}";

    public IResult<object> Resolve(object? value)
    {
        if (value == null) return Result.Ok();
        return (multipleOf % ((int)value)) == 0 ? Result.Ok(value) : Result.Err(Message);
    }
}