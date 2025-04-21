using System.Text.Json;
using System.Text.Json.Serialization;

namespace Rum.Graph;

public class Error
{
    [JsonPropertyName("key")]
    [JsonPropertyOrder(0)]
    public string? Key { get; set; }

    [JsonPropertyName("message")]
    [JsonPropertyOrder(1)]
    public string? Message { get; set; }

    [JsonPropertyName("errors")]
    [JsonPropertyOrder(2)]
    public IList<Error> Errors { get; set; } = [];

    public Error Add(Error error)
    {
        Errors.Add(error);
        return this;
    }

    public Error Add(string message)
    {
        var error = new Error() { Message = message };
        return Add(error);
    }

    public Error Add(string key, string message)
    {
        var error = new Error() { Key = key, Message = message };
        return Add(error);
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