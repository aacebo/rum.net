using Rum.Core;

namespace Rum.Schemas.Rules;

/// <summary>
/// value must not be null
/// </summary>
public class Required : IRule
{
    public string Name => "required";

    public IResult<object?> Resolve(object? value)
    {
        return value != null ? Result<object?>.Ok(value) : Result<object?>.Err(Name);
    }
}