using System.Text.RegularExpressions;

using Rum.Core;

namespace Rum.Data.Rules.String;

/// <summary>
/// Regex Pattern Matching
/// </summary>
public class Pattern : IRule
{
    public string Name => "string.pattern";
    public string Message => $"must match pattern '{_pattern}'";

    private readonly Regex _pattern;

    public Pattern(string pattern) => _pattern = new Regex(pattern);
    public Pattern(Regex pattern) => _pattern = pattern;

    public IResult<object> Resolve(object? value)
    {
        if (value == null) return Result.Ok();
        return _pattern.IsMatch((string)value) ? Result.Ok(value) : Result.Err(Message);
    }
}