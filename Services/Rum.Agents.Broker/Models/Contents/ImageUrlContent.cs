using Rum.Text.Json;

namespace Rum.Agents.Broker.Models;

public static partial class Contents
{
    public static ImageUrlContent ImageUrl(string imageUrl) => new(imageUrl);
}

public partial class ContentType
{
    public static readonly ContentType ImageUrl = new("image_url");
    public bool IsImageUrl => ImageUrl.Equals(Value);
}

[JsonObject<IImageUrlContent>]
public interface IImageUrlContent : IContent
{
    public string ImageUrl { get; }
}

public class ImageUrlContent(string imageUrl) : IImageUrlContent
{
    public ContentType Type => ContentType.ImageUrl;
    public string ImageUrl { get; set; } = imageUrl;
}