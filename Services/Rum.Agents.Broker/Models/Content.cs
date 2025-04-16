using Rum.Text;
using Rum.Text.Json;

namespace Rum.Agents.Broker.Models;

public partial class ContentType(string value) : StringEnum(value)
{
}

/// <summary>
/// some piece of content sent from one
/// agent to another
/// </summary>
[JsonObject<IContent>]
public interface IContent
{
    /// <summary>
    /// the content type of the content
    /// </summary>
    public ContentType Type { get; }
}