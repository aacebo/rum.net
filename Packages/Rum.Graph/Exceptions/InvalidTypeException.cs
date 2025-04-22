using System.Text.Json.Serialization;

namespace Rum.Graph.Exceptions;

public class InvalidTypeException<T>(string? message = null) : InvalidTypeException(typeof(T), message)
{
}

public class InvalidTypeException : Exception
{
    [JsonPropertyName("rule")]
    [JsonPropertyOrder(0)]
    public string Rule => "type";

    [JsonPropertyName("type")]
    [JsonPropertyOrder(1)]
    public string Type { get; }

    public InvalidTypeException(Type type, string? message = null) : base(message)
    {
        Type = type.Name;
    }
}