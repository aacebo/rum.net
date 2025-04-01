using System.Text.Json;
using System.Text.Json.Serialization;

namespace Rum.Core;

/// <summary>
/// A Group Of Errors
/// </summary>
public class ErrorGroup : IError
{
    [JsonPropertyName("message")]
    [JsonPropertyOrder(0)]
    public string? Message { get; set; }

    [JsonPropertyName("errors")]
    [JsonPropertyOrder(1)]
    public IList<IError>? Errors { get; set; }

    [JsonIgnore]
    public bool Empty => Errors == null || Errors.Count == 0;

    [JsonIgnore]
    public int Count => Errors?.Count ?? 0;

    public ErrorGroup(string? message = null)
    {
        Message = message;
    }

    public ErrorGroup(params IError[] errors)
    {
        Errors = errors;
    }

    public ErrorGroup(string message, params IError[] errors)
    {
        Message = message;
        Errors = errors;
    }

    public ErrorGroup Add(IError error)
    {
        Errors ??= Errors ?? [];
        Errors.Add(error);
        return this;
    }

    public ErrorGroup Add(string message)
    {
        return Add(new Error(message));
    }

    public string GetError() => ToString();
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, new JsonSerializerOptions()
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        });
    }
}