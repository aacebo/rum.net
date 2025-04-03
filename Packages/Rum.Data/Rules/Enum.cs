using Rum.Core;

namespace Rum.Data.Rules;

/// <summary>
/// value must be one of the options
/// </summary>
public class Enum<T>(params T[] options) : IRule
{
    public string Name => "enum";
    public T[] Options { get; } = options;
    public string Message => $"must be one of [{string.Join(", ", Options.Select(o => o?.ToString()))}]";

    public IResult<object> Resolve(object? value)
    {
        if (value == null) return Result.Ok();
        return Options.Contains((T?)value) ? Result.Ok(value) : Result.Err(Message);
    }
}