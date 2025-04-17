using System.Text.Json;
using System.Text.Json.Serialization;

namespace Rum.Graph.Parsing;

public class ParseException : Exception
{
    [JsonPropertyName("line")]
    [JsonPropertyOrder(0)]
    public int Line { get; set; }

    [JsonPropertyName("start")]
    [JsonPropertyOrder(1)]
    public int Start { get; set; }

    [JsonPropertyName("end")]
    [JsonPropertyOrder(2)]
    public int End { get; set; }

    [JsonPropertyName("message")]
    [JsonPropertyOrder(3)]
    public override string Message { get; }

    public ParseException(int line, int start, int end, params string[] message) : base()
    {
        Line = line;
        Start = start;
        End = end;
        Message = string.Join("\n", message);
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