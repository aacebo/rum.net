using System.Text.Json.Serialization;

namespace Rum.Agents.Cli.Models;

public class Task
{
    [JsonPropertyName("name")]
    [JsonPropertyOrder(0)]
    public required string Name { get; set; }

    [JsonPropertyName("description")]
    [JsonPropertyOrder(1)]
    public string? Description { get; set; }
}