using System.Text.Json.Serialization;

namespace Rum.Cmd.Elements;

public class Text(string content) : IElement
{
    [JsonPropertyName("content")]
    [JsonPropertyOrder(0)]
    public string Content { get; set; } = content;

    public string Render()
    {
        return Content;
    }
}