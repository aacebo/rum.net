using System.Text.Json.Serialization;

using SqlKata;

namespace Rum.Agents.Broker.Models;

public class Skill
{
    [JsonPropertyName("id")]
    [JsonPropertyOrder(0)]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [JsonPropertyName("name")]
    [JsonPropertyOrder(1)]
    [Column("name")]
    public required string Name { get; set; }

    [JsonPropertyName("description")]
    [JsonPropertyOrder(2)]
    [Column("description")]
    public string? Description { get; set; }

    [JsonPropertyName("created_at")]
    [JsonPropertyOrder(3)]
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [JsonPropertyName("updated_at")]
    [JsonPropertyOrder(4)]
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
