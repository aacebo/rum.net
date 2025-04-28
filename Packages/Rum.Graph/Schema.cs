using System.Text.Json.Serialization;

namespace Rum.Graph;

public class Schema
{
    [JsonPropertyName("type")]
    [JsonPropertyOrder(0)]
    public required string Type { get; set; }

    [JsonPropertyName("fields")]
    [JsonPropertyOrder(1)]
    public IDictionary<string, Schema>? Fields { get; set; }
}