using System.Text.Json;
using System.Text.Json.Serialization;

namespace Rum.Graph;

public class Query
{
    [JsonPropertyName("args")]
    [JsonPropertyOrder(0)]
    public ArgumentCollection Args { get; set; } = [];

    [JsonPropertyName("fields")]
    [JsonPropertyOrder(1)]
    public FieldCollection Fields { get; set; } = [];

    public override string ToString()
    {
        return JsonSerializer.Serialize(this, new JsonSerializerOptions()
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        });
    }
}