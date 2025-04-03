using Rum.Core;

namespace Rum.Data.Rules;

/// <summary>
/// value must not be null
/// </summary>
public class Required : IRule
{
    public string Name => "required";

    public IResult<object> Resolve(object? value)
    {
        return value != null ? Result.Ok(value) : Result.Err(Name);
    }
}