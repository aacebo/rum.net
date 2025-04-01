using System.Text.Json;
using System.Text.Json.Serialization;

namespace Rum.Core;

/// <summary>
/// A Group Of Errors
/// </summary>
public class ErrorGroup : IError
{
    [JsonIgnore]
    public bool Empty => Errors == null || Errors.Count == 0;

    [JsonIgnore]
    public int Count => Errors?.Count ?? 0;

    [JsonIgnore]
    public string Message => ToString();

    [JsonPropertyName("errors")]
    [JsonPropertyOrder(0)]
    public IList<IError>? Errors { get; set; }

    public ErrorGroup Add(IError error)
    {
        Errors ??= Errors ?? [];
        Errors.Add(error);
        return this;
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