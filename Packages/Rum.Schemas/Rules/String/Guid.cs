using Rum.Core;

namespace Rum.Schemas.Rules.String;

/// <summary>
/// Guid/UUID String Format
/// </summary>
public class Guid : IRule
{
    public string Name => "string.guid";
    public string Message => "must be a valid GUID (UUID)";

    public IResult<object?> Resolve(object? value)
    {
        if (value == null) return Result<object?>.Ok();
        return System.Guid.TryParse((string)value, out var _) ? Result<object?>.Ok(value) : Result<object?>.Err(Message);
    }
}