using Rum.Core;

namespace Rum.Schemas.Rules;

/// <summary>
/// expect provided rules to resolve false
/// </summary>
public class Not(params IRule[] rules) : IRule
{
    public string Name => "not";
    public string Message => $"[{string.Join(", ", rules.Select(r => r.Name))}] should be false";

    public IResult<object?> Resolve(object? value)
    {
        foreach (var rule in rules)
        {
            var res = rule.Resolve(value);

            if (res.Error == null)
            {
                return Result<object?>.Err(Message);
            }
        }

        return Result<object?>.Ok(value);
    }
}