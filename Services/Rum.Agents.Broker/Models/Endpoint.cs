using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using SqlKata;

namespace Rum.Agents.Broker.Models;

public class Endpoint
{
    [JsonPropertyName("agent_id")]
    [JsonPropertyOrder(0)]
    [Column("agent_id")]
    public required string AgentId { get; set; }

    [JsonPropertyName("path")]
    [JsonPropertyOrder(1)]
    [Column("path")]
    public required string Path { get; set; }

    [JsonPropertyName("dialect")]
    [JsonPropertyOrder(2)]
    [Column("dialect")]
    public required string Dialect { get; set; }

    [JsonPropertyName("created_at")]
    [JsonPropertyOrder(3)]
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [JsonPropertyName("updated_at")]
    [JsonPropertyOrder(4)]
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public class CreateRequest
    {
        [JsonPropertyName("path")]
        [JsonPropertyOrder(1)]
        public required string Path { get; set; }

        [JsonPropertyName("dialect")]
        [JsonPropertyOrder(2)]
        [AllowedValues("a2a", "mcp")]
        public required string Dialect { get; set; }
    }
}