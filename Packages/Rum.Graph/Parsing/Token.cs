using System.Text;
using System.Text.Json.Serialization;

namespace Rum.Graph.Parsing;

public class Token
{
    [JsonPropertyName("type")]
    [JsonPropertyOrder(0)]
    public TokenType Type { get; set; }

    [JsonPropertyName("line")]
    [JsonPropertyOrder(1)]
    public int Line { get; set; }

    [JsonPropertyName("start")]
    [JsonPropertyOrder(2)]
    public int Start { get; set; }

    [JsonPropertyName("end")]
    [JsonPropertyOrder(3)]
    public int End { get; set; }

    [JsonPropertyName("value")]
    [JsonPropertyOrder(4)]
    public byte[] Value { get; set; } = [];

    public byte ToByte() => Value.Length == 1 ? Value.First() : throw ToError("expected byte");
    public int ToInt() => int.TryParse(Value, out var value) ? value : throw ToError("expected int");
    public float ToFloat() => float.TryParse(Value, out var value) ? value : throw ToError("expected float");
    public bool ToBool() => bool.TryParse(Encoding.UTF8.GetChars(Value), out var value) ? value : throw ToError("expected bool");
    public override string ToString() => Encoding.UTF8.GetString(Value);
    public ParseException ToError(params string[] message) => new ParseException(Line, Start, End, message);
}