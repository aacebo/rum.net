using System.Text.Json.Serialization;

using SqlKata;

namespace Rum.Agents.Broker.Models;

public class User
{
    [JsonPropertyName("id")]
    [JsonPropertyOrder(0)]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [JsonPropertyName("name")]
    [JsonPropertyOrder(1)]
    [Column("name")]
    public required string Name { get; set; }

    [JsonPropertyName("created_at")]
    [JsonPropertyOrder(2)]
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public class CreateRequest
    {
        [JsonPropertyName("name")]
        [JsonPropertyOrder(0)]
        public required string Name { get; set; }
    }
}