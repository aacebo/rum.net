namespace Rum.Text;

public class Ansi(string value) : StringEnum(value)
{
    public static readonly Ansi Reset = new("\x1b[0m");

    public static readonly Ansi Bold = new("\x1b[1m");
    public static readonly Ansi BoldReset = new("\x1b[22m");
    public static readonly Ansi Italic = new("\x1b[3m");
    public static readonly Ansi ItalicReset = new("\x1b[23m");
    public static readonly Ansi Underline = new("\x1b[4m");
    public static readonly Ansi UnderlineReset = new("\x1b[24m");
    public static readonly Ansi Strike = new("\x1b[9m");
    public static readonly Ansi StrikeReset = new("\x1b[29m");

    public static readonly Ansi ForegroundReset = new("\x1b[0m");
    public static readonly Ansi BackgroundReset = new("\x1b[0m");
    public static readonly Ansi ForegroundBlack = new("\x1b[30m");
    public static readonly Ansi BackgroundBlack = new("\x1b[40m");
    public static readonly Ansi ForegroundRed = new("\x1b[31m");
    public static readonly Ansi BackgroundRed = new("\x1b[41m");
    public static readonly Ansi ForegroundGreen = new("\x1b[32m");
    public static readonly Ansi BackgroundGreen = new("\x1b[42m");
    public static readonly Ansi ForegroundYellow = new("\x1b[33m");
    public static readonly Ansi BackgroundYellow = new("\x1b[43m");
    public static readonly Ansi ForegroundBlue = new("\x1b[34m");
    public static readonly Ansi BackgroundBlue = new("\x1b[44m");
    public static readonly Ansi ForegroundMagenta = new("\x1b[35m");
    public static readonly Ansi BackgroundMagenta = new("\x1b[45m");
    public static readonly Ansi ForegroundCyan = new("\x1b[36m");
    public static readonly Ansi BackgroundCyan = new("\x1b[46m");
    public static readonly Ansi ForegroundWhite = new("\x1b[37m");
    public static readonly Ansi BackgroundWhite = new("\x1b[47m");
    public static readonly Ansi ForegroundGray = new("\x1b[90m");
    public static readonly Ansi ForegroundDefault = new("\x1b[39m");
    public static readonly Ansi BackgroundDefault = new("\x1b[49m");
}