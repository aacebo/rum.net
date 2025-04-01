using System.Text.RegularExpressions;

using Rum.Core;

namespace Rum.Data;

/// <summary>
/// Schema used to validate strings
/// </summary>
public class String : Any<string?>, ISchema<string?>
{
    public override string Name => "string";

    public String() : base()
    {
        Rule(new Rule("string", value =>
        {
            if (value == null) return Result<object?>.Ok();
            if (value is string asString) return Result<object?>.Ok(asString);
            return Result<object?>.Err("must be a string");
        }));
    }

    public override String Rule(IRule rule) => (String)base.Rule(rule);
    public override String Rule(string name, Rule.ResolverFn resolve) => (String)base.Rule(name, resolve);
    public override String Required() => (String)base.Required();
    public override String Enum(params string?[] options) => (String)base.Enum(options);
    public override String Default(string? defaultValue) => (String)base.Default(defaultValue);
    public override String Not(params IRule[] rules) => (String)base.Not(rules);

    public String Min(int min) => Rule(new Rules.String.Min(min));
    public String Max(int max) => Rule(new Rules.String.Max(max));
    public String Length(int length) => Rule(new Rules.String.Length(length));
    public String Pattern(string pattern) => Rule(new Rules.String.Pattern(pattern));
    public String Pattern(Regex pattern) => Rule(new Rules.String.Pattern(pattern));
    public String Email() => Rule(new Rules.String.Email());
    public String Guid() => Rule(new Rules.String.Guid());
    public String Url() => Rule(new Rules.String.Url());
}