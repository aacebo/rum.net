using Rum.Text.Json;

namespace Rum.Agents.Broker.Models;

public static partial class Contents
{
    public static TextContent Text(string text) => new(text);
}

public partial class ContentType
{
    public static readonly ContentType Text = new("text");
    public bool IsText => Text.Equals(Value);
}

[JsonObject<ITextContent>]
public interface ITextContent : IContent
{
    public string Text { get; }
}

public class TextContent(string text) : ITextContent
{
    public ContentType Type => ContentType.Text;
    public string Text { get; set; } = text;
}