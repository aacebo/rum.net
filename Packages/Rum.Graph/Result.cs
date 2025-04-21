using System.Text.Json;
using System.Text.Json.Serialization;

namespace Rum.Graph;

public class Result : Result<object>
{
    public T GetData<T>()
    {
        return TryGetData<T>() ?? throw new InvalidCastException();
    }

    public T? TryGetData<T>()
    {
        return (T?)Data;
    }

    public new static Result Ok(object? data = null)
    {
        return new() { Data = data };
    }

    public new static Result Err(Error error)
    {
        return new() { Error = error };
    }

    public new static Result Err(string key, params string[] message)
    {
        return new()
        {
            Error = new Error()
            {
                Key = key,
                Message = string.Join("\n", message)
            }
        };
    }

    public new static Result Err(params string[] message)
    {
        return new()
        {
            Error = new Error()
            {
                Message = string.Join("\n", message)
            }
        };
    }
}

public class Result<T> where T : notnull
{
    [JsonPropertyName("$meta")]
    [JsonPropertyOrder(0)]
    public MetaData Meta { get; set; } = [];

    [JsonPropertyName("data")]
    [JsonPropertyOrder(1)]
    public T? Data { get; set; }

    [JsonPropertyName("error")]
    [JsonPropertyOrder(2)]
    public Error? Error { get; set; }

    [JsonIgnore]
    public bool IsError => Error != null;

    public override string ToString()
    {
        return JsonSerializer.Serialize(this, new JsonSerializerOptions()
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        });
    }

    public static Result<T> Ok(T? data = default)
    {
        return new() { Data = data };
    }

    public static Result<T> Err(Error error)
    {
        return new() { Error = error };
    }

    public static Result<T> Err(string key, params string[] message)
    {
        return new()
        {
            Error = new Error()
            {
                Key = key,
                Message = string.Join("\n", message)
            }
        };
    }

    public static Result<T> Err(params string[] message)
    {
        return new()
        {
            Error = new Error()
            {
                Message = string.Join("\n", message)
            }
        };
    }
}
