using Rum.Core;

namespace Rum.Data.Rules.String;

/// <summary>
/// Email String Format
/// </summary>
public class Email : IRule
{
    public string Name => "string.email";
    public string Message => $"must be a valid email address";

    public IResult<object> Resolve(object? value)
    {
        if (value == null) return Result.Ok();

        try
        {
            var address = new System.Net.Mail.MailAddress((string)value);
            return Result.Ok(value);
        }
        catch
        {
            return Result.Err(Message);
        }
    }
}