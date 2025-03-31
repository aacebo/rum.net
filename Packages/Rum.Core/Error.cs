using System.Text.Json.Serialization;

namespace Rum.Core;

/// <summary>
/// Any Error
/// </summary>
public interface IError
{
    /// <summary>
    /// the error message
    /// </summary>
    public string Message { get; }
}

/// <summary>
/// Error
/// </summary>
/// <param name="message">the error message</param>
public class Error(string message) : IError
{
    [JsonPropertyName("message")]
    [JsonPropertyOrder(0)]
    public string Message { get; } = message;
}