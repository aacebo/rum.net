using Rum.Core;

namespace Rum.Data.Rules.String;

/// <summary>
/// URL String Format
/// </summary>
public class Url : IRule
{
    public string Name => "string.url";
    public string Message => "must be a valid URL";

    public IResult<object?> Resolve(object? value)
    {
        if (value == null) return Result<object?>.Ok();
        return Uri.TryCreate((string)value, UriKind.Absolute, out var _) ? Result<object?>.Ok(value) : Result<object?>.Err(Message);
    }
}