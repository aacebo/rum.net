using System.Text.RegularExpressions;

using Rum.Core;

namespace Rum.Data;

public static partial class Schemas
{
    /// <summary>
    /// Schema used to validate strings
    /// </summary>
    public static StringSchema String() => new();
}

/// <summary>
/// Schema used to validate strings
/// </summary>
public class StringSchema : AnySchema<string?>, ISchema<string?>
{
    public override string Name => "string";

    public StringSchema() : base()
    {
        Rule(new Rule("string", value =>
        {
            if (value == null) return Result<object?>.Ok();
            if (value is string asString) return Result<object?>.Ok(asString);
            return Result<object?>.Err("must be a string");
        }));
    }
    public override StringSchema Message(string message) => (StringSchema)base.Message(message);
    public override StringSchema Rule(IRule rule) => (StringSchema)base.Rule(rule);
    public override StringSchema Rule(string name, Rule.ResolverFn resolve) => (StringSchema)base.Rule(name, resolve);
    public override StringSchema Required() => (StringSchema)base.Required();
    public override StringSchema Enum(params string?[] options) => (StringSchema)base.Enum(options);
    public override StringSchema Default(string? defaultValue) => (StringSchema)base.Default(defaultValue);

    public StringSchema Min(int min) => Rule(new Rules.String.Min(min));
    public StringSchema Max(int max) => Rule(new Rules.String.Max(max));
    public StringSchema Length(int length) => Rule(new Rules.String.Length(length));
    public StringSchema Pattern(string pattern) => Rule(new Rules.String.Pattern(pattern));
    public StringSchema Pattern(Regex pattern) => Rule(new Rules.String.Pattern(pattern));
    public StringSchema Email() => Rule(new Rules.String.Email());
    public StringSchema Guid() => Rule(new Rules.String.Guid());
    public StringSchema Url() => Rule(new Rules.String.Url());
}