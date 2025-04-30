using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

using Rum.Graph.Exceptions;
using Rum.Graph.Extensions;

namespace Rum.Graph;

public class Schema
{
    [JsonPropertyName("type")]
    [JsonPropertyOrder(0)]
    public string Type { get; set; }

    [JsonPropertyName("required")]
    [JsonPropertyOrder(1)]
    public bool? Required { get; set; }

    [JsonPropertyName("fields")]
    [JsonPropertyOrder(2)]
    public IDictionary<string, Schema>? Fields { get; set; }

    public Schema(string type, bool? required = null)
    {
        Type = type;
        Required = required;
    }

    public Schema(Type type)
    {
        Type = type.GetHumanFriendlyName();
    }

    public Schema(MemberInfo member)
    {
        Type = member.GetPropertyOrFieldType().GetHumanFriendlyName();
        Required = member.IsOptional() ? null : true;
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this, new JsonSerializerOptions()
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        });
    }
}