namespace Rum.Graph.Parsing;

public static class ByteExtensions
{
    public static bool IsInt(this byte value)
    {
        return value >= '0' && value <= '9';
    }

    public static bool IsAlpha(this byte value)
    {
        return (
            (value >= 'a' && value <= 'z') ||
            (value >= 'A' && value <= 'Z') ||
            value == '_'
        );
    }
}