using System.Text;

namespace Rum.Text;

public static partial class StringBuilderExtensions
{
    public static StringBuilder EndLine(this StringBuilder builder, int count = 1)
    {
        for (var i = 0; i < count; i++)
        {
            builder = builder.Append('\n');
        }

        return builder;
    }
}