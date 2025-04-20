using System.Text.Json.Serialization;

using SqlKata;

namespace Rum.Agents.Broker.Models;

public class Endpoint
{
    [JsonPropertyName("agent_id")]
    [JsonPropertyOrder(0)]
    [Column("agent_id")]
    public required string AgentId { get; set; }

    [JsonPropertyName("dialect")]
    [JsonPropertyOrder(1)]
    [Column("dialect")]
    public required Dialect Dialect { get; set; }

    [JsonPropertyName("path")]
    [JsonPropertyOrder(2)]
    [Column("path")]
    public required string Path { get; set; }

    [JsonPropertyName("created_at")]
    [JsonPropertyOrder(3)]
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [JsonPropertyName("updated_at")]
    [JsonPropertyOrder(4)]
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}