using System.Text.Json;
using System.Text.Json.Serialization;

namespace Rum.Graph;

public class Result
{
    [JsonPropertyName("$meta")]
    [JsonPropertyOrder(0)]
    public MetaData Meta { get; set; } = [];

    [JsonPropertyName("data")]
    [JsonPropertyOrder(1)]
    public object? Data { get; set; }

    [JsonPropertyName("error")]
    [JsonPropertyOrder(2)]
    public Error? Error { get; set; }

    [JsonIgnore]
    public bool IsError => Error != null;

    public void Deconstruct(out object? data, out Error? error)
    {
        data = Data;
        error = Error;
    }

    public void Deconstruct(out MetaData meta, out object? data, out Error? error)
    {
        meta = Meta;
        data = Data;
        error = Error;
    }

    public T GetData<T>()
    {
        return TryGetData<T>() ?? throw new InvalidCastException();
    }

    public T? TryGetData<T>()
    {
        return (T?)Data;
    }

    public Result Merge(Result from)
    {
        Meta.Merge(from.Meta);

        if (from.Data is not null)
        {
            Data = from.Data;
        }

        if (from.Error is not null)
        {
            Error = from.Error;
        }

        return this;
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this, new JsonSerializerOptions()
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        });
    }

    public static Result Ok(object? data = null)
    {
        return new() { Data = data };
    }

    public static Result Err(Error error)
    {
        return new() { Error = error };
    }

    public static Result Err(string message)
    {
        return new()
        {
            Error = new Error()
            {
                Message = message
            }
        };
    }

    public static Result Err(string key, string message)
    {
        return new()
        {
            Error = new Error()
            {
                Key = key,
                Message = message
            }
        };
    }
}