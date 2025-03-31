using Rum.Core;

namespace Rum.Schemas.Rules.String;

public class Email : IRule
{
    public string Name => "string.email";
    public string Message => $"must be a valid email address";

    public IResult<object?> Resolve(object? value)
    {
        if (value == null) return Result<object?>.Ok();

        try
        {
            var address = new System.Net.Mail.MailAddress((string)value);
            return Result<object?>.Ok(value);
        }
        catch
        {
            return Result<object?>.Err(Message);
        }
    }
}