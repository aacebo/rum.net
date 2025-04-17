namespace Rum.Graph.Parsing;

public enum TokenType : byte
{
    Eof,

    // special characters
    Comma,
    Colon,
    LeftParen,
    RightParen,
    LeftBrace,
    RightBrace,

    // types
    Identifier,
    String,
    Byte,
    Int,
    Float,
    Null,
    Bool
}

public static class TokenTypes
{
    public static TokenType GetKeywordToken(string value)
    {
        return value switch
        {
            "true" or "false" => TokenType.Bool,
            _ => TokenType.Eof
        };
    }

    public static byte ToByte(this TokenType type)
    {
        return (byte)type;
    }
}