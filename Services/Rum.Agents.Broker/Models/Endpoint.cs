using System.Text.Json.Serialization;

using SqlKata;

namespace Rum.Agents.Broker.Models;

public class Endpoint
{
    [JsonPropertyName("agent_id")]
    [JsonPropertyOrder(0)]
    [Column("agent_id")]
    public required string AgentId { get; set; }
}