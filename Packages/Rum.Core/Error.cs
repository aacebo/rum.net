using System.Text.Json.Serialization;

namespace Rum.Core;

/// <summary>
/// Any Error
/// </summary>
public interface IError
{
    /// <summary>
    /// get the error in string form
    /// </summary>
    public string GetError();
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

    public string GetError() => Message;
}