using Rum.Text.Json;

namespace Rum.Agents.Broker.Models;

public static partial class Contents
{
    public static MediaContent Media(Stream stream) => new(stream);
}

public partial class ContentType
{
    public static readonly ContentType Media = new("media");
    public bool IsMedia => Media.Equals(Value);
}

[JsonObject<IMediaContent>]
public interface IMediaContent : IContent
{
    public Stream Stream { get; }
}

public class MediaContent(Stream stream) : IMediaContent
{
    public ContentType Type => ContentType.Media;
    public Stream Stream { get; set; } = stream;
}