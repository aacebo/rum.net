using System.Text.Json.Serialization;

namespace Rum.Agents.Broker.Models;

public enum Dialect
{
    [JsonStringEnumMemberName("a2a")]
    A2A,

    [JsonStringEnumMemberName("mcp")]
    Mcp,
}