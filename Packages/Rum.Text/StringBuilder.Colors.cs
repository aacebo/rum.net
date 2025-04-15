using System.Text;

namespace Rum.Text;

public static partial class StringBuilderExtensions
{
    public static StringBuilder Reset(this StringBuilder builder)
    {
        return builder.Append(Ansi.Reset);
    }

    public static StringBuilder Append(this StringBuilder builder, Ansi code, string text)
    {
        return builder.Append(code).Append(text).Append(Ansi.Reset);
    }

    public static StringBuilder Bold(this StringBuilder builder, string text)
    {
        return builder.Append(Ansi.Bold).Append(text).Append(Ansi.BoldReset);
    }

    public static StringBuilder Italic(this StringBuilder builder, string text)
    {
        return builder.Append(Ansi.Italic).Append(text).Append(Ansi.ItalicReset);
    }

    public static StringBuilder Underline(this StringBuilder builder, string text)
    {
        return builder.Append(Ansi.Underline).Append(text).Append(Ansi.UnderlineReset);
    }

    public static StringBuilder Strike(this StringBuilder builder, string text)
    {
        return builder.Append(Ansi.Strike).Append(text).Append(Ansi.StrikeReset);
    }

    public static StringBuilder Black(this StringBuilder builder, string text)
    {
        return builder.Append(Ansi.ForegroundBlack).Append(text).Append(Ansi.ForegroundReset);
    }

    public static StringBuilder BgBlack(this StringBuilder builder, string text)
    {
        return builder.Append(Ansi.BackgroundBlack).Append(text).Append(Ansi.BackgroundReset);
    }

    public static StringBuilder Red(this StringBuilder builder, string text)
    {
        return builder.Append(Ansi.ForegroundRed).Append(text).Append(Ansi.ForegroundReset);
    }

    public static StringBuilder BgRed(this StringBuilder builder, string text)
    {
        return builder.Append(Ansi.BackgroundRed).Append(text).Append(Ansi.BackgroundReset);
    }

    public static StringBuilder Green(this StringBuilder builder, string text)
    {
        return builder.Append(Ansi.ForegroundGreen).Append(text).Append(Ansi.ForegroundReset);
    }

    public static StringBuilder BgGreen(this StringBuilder builder, string text)
    {
        return builder.Append(Ansi.BackgroundGreen).Append(text).Append(Ansi.BackgroundReset);
    }

    public static StringBuilder Yellow(this StringBuilder builder, string text)
    {
        return builder.Append(Ansi.ForegroundYellow).Append(text).Append(Ansi.ForegroundReset);
    }

    public static StringBuilder BgYellow(this StringBuilder builder, string text)
    {
        return builder.Append(Ansi.BackgroundYellow).Append(text).Append(Ansi.BackgroundReset);
    }

    public static StringBuilder Blue(this StringBuilder builder, string text)
    {
        return builder.Append(Ansi.ForegroundBlue).Append(text).Append(Ansi.ForegroundReset);
    }

    public static StringBuilder BgBlue(this StringBuilder builder, string text)
    {
        return builder.Append(Ansi.BackgroundBlue).Append(text).Append(Ansi.BackgroundReset);
    }

    public static StringBuilder Magenta(this StringBuilder builder, string text)
    {
        return builder.Append(Ansi.ForegroundMagenta).Append(text).Append(Ansi.ForegroundReset);
    }

    public static StringBuilder BgMagenta(this StringBuilder builder, string text)
    {
        return builder.Append(Ansi.BackgroundMagenta).Append(text).Append(Ansi.BackgroundReset);
    }

    public static StringBuilder Cyan(this StringBuilder builder, string text)
    {
        return builder.Append(Ansi.ForegroundCyan).Append(text).Append(Ansi.ForegroundReset);
    }

    public static StringBuilder BgCyan(this StringBuilder builder, string text)
    {
        return builder.Append(Ansi.BackgroundCyan).Append(text).Append(Ansi.BackgroundReset);
    }

    public static StringBuilder White(this StringBuilder builder, string text)
    {
        return builder.Append(Ansi.ForegroundWhite).Append(text).Append(Ansi.ForegroundReset);
    }

    public static StringBuilder BgWhite(this StringBuilder builder, string text)
    {
        return builder.Append(Ansi.BackgroundWhite).Append(text).Append(Ansi.BackgroundReset);
    }

    public static StringBuilder Gray(this StringBuilder builder, string text)
    {
        return builder.Append(Ansi.ForegroundGray).Append(text).Append(Ansi.ForegroundReset);
    }

    public static StringBuilder Default(this StringBuilder builder, string text)
    {
        return builder.Append(Ansi.ForegroundDefault).Append(text).Append(Ansi.ForegroundReset);
    }

    public static StringBuilder BgDefault(this StringBuilder builder, string text)
    {
        return builder.Append(Ansi.BackgroundDefault).Append(text).Append(Ansi.BackgroundReset);
    }
}