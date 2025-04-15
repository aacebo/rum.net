namespace Rum.Cmd;

public class ParseException : Exception
{
    public ParseException(string? message = null) : base(message ?? "parse exception")
    {

    }
}