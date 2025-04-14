using System.Text.Json.Serialization;

using SqlKata;
using String = Rum.Data.Annotations.String;

namespace Rum.Agents.Broker.Models;

public class Agent
{
    [JsonPropertyName("id")]
    [JsonPropertyOrder(0)]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [JsonPropertyName("version")]
    [JsonPropertyOrder(1)]
    [Column("version")]
    public required string Version { get; set; }

    [JsonPropertyName("name")]
    [JsonPropertyOrder(2)]
    [Column("name")]
    public required string Name { get; set; }

    [JsonPropertyName("description")]
    [JsonPropertyOrder(3)]
    [Column("description")]
    public string? Description { get; set; }

    [JsonPropertyName("url")]
    [JsonPropertyOrder(4)]
    [Column("url")]
    public required string Url { get; set; }

    [JsonPropertyName("documentation_url")]
    [JsonPropertyOrder(5)]
    [Column("documentation_url")]
    public string? DocumentationUrl { get; set; }

    [JsonPropertyName("created_at")]
    [JsonPropertyOrder(6)]
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [JsonPropertyName("updated_at")]
    [JsonPropertyOrder(7)]
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public class CreateRequest
    {
        [JsonPropertyName("version")]
        [JsonPropertyOrder(0)]
        [String.Semver]
        public string Version { get; set; } = "0.0.0";

        [JsonPropertyName("name")]
        [JsonPropertyOrder(1)]
        public required string Name { get; set; }

        [JsonPropertyName("description")]
        [JsonPropertyOrder(2)]
        public string? Description { get; set; }

        [JsonPropertyName("url")]
        [JsonPropertyOrder(3)]
        [String.Url]
        public required string Url { get; set; }

        [JsonPropertyName("documentation_url")]
        [JsonPropertyOrder(4)]
        [String.Url]
        public string? DocumentationUrl { get; set; }
    }
}