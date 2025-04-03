using System.Text.Json;
using System.Text.Json.Serialization;

namespace Rum.Core;

/// <summary>
/// Any Result
/// </summary>
public interface IResult : IResult<object>;

/// <summary>
/// Any Result
/// </summary>
public interface IResult<T>
{
    /// <summary>
    /// the result value
    /// </summary>
    [JsonPropertyName("value")]
    [JsonPropertyOrder(0)]
    public T? Value { get; set; }

    /// <summary>
    /// the result error
    /// </summary>
    [JsonPropertyName("error")]
    [JsonPropertyOrder(1)]
    public IError? Error { get; set; }
}

/// <summary>
/// Some Result
/// </summary>
public class Result : Result<object>, IResult<object>;

/// <summary>
/// Some Result
/// </summary>
public class Result<T> : IResult<T>
{
    /// <summary>
    /// the result value
    /// </summary>
    [JsonPropertyName("value")]
    [JsonPropertyOrder(0)]
    public T? Value { get; set; }

    /// <summary>
    /// the result error
    /// </summary>
    [JsonPropertyName("error")]
    [JsonPropertyOrder(1)]
    public IError? Error { get; set; }

    public Result() { }
    public Result(T? value) => Value = value;
    public Result(IError error) => Error = error;
    public Result(IResult<object> result)
    {
        Error = result.Error;
        Value = (T?)result.Value;
    }

    public Result<T> WithValue(T value)
    {
        Value = value;
        return this;
    }

    public Result<T> WithError(IError error)
    {
        Error = error;
        return this;
    }

    public Result<T> WithError(string message)
    {
        Error = new Error(message);
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

    public static IResult<T> Ok() => new Result<T>();
    public static IResult<T> Ok(T? value) => new Result<T>(value);
    public static IResult<T> Err(IError error) => new Result<T>(error);
    public static IResult<T> Err(string message) => new Result<T>(new Error(message));
}