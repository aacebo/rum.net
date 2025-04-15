using System.Reflection;
using System.Text.Json.Serialization;

namespace Rum.Cmd;

[AttributeUsage(
    AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Field | AttributeTargets.Property,
    Inherited = true
)]
public class CommandAttribute(string? Name = null) : Attribute
{
    [JsonPropertyName("name")]
    [JsonPropertyOrder(0)]
    public string? Name { get; set; } = Name;

    [JsonPropertyName("version")]
    [JsonPropertyOrder(1)]
    public string? Version { get; set; } = Assembly.GetEntryAssembly()?.GetName()?.Version?.ToString();

    [JsonPropertyName("aliases")]
    [JsonPropertyOrder(1)]
    public string[] Aliases { get; set; } = [];

    [JsonPropertyName("description")]
    [JsonPropertyOrder(2)]
    public string? Description { get; set; }

    public bool Select(string key) => key == Name || Aliases.Contains(key);
}