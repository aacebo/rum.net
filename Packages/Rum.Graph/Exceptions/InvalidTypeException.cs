using System.Text.Json.Serialization;

namespace Rum.Graph.Exceptions;

public class InvalidTypeException<T> : Exception
{
    [JsonPropertyName("rule")]
    [JsonPropertyOrder(0)]
    public string Rule => "type";

    [JsonPropertyName("type")]
    [JsonPropertyOrder(1)]
    public string Type { get; }

    public InvalidTypeException(string? message = null) : base(message)
    {
        Type = typeof(T).Name;
    }
}