using System.Reflection;
using System.Text.Json.Serialization;

using Rum.Core;

namespace Rum.Data;

public static partial class Schemas
{
    /// <summary>
    /// Schema used to validate objects
    /// </summary>
    public static ObjectSchema Object(IDictionary<string, IRule>? properties = null) => new(properties);
}

/// <summary>
/// Schema used to validate objects
/// </summary>
public class ObjectSchema : AnySchema<object?>, ISchema<object?>
{
    public override string Name => "object";

    protected IDictionary<string, IRule> Properties { get; set; }

    public ObjectSchema(IDictionary<string, IRule>? properties = null) : base()
    {
        Properties = properties ?? new Dictionary<string, IRule>();
    }

    public override ObjectSchema Message(string message) => (ObjectSchema)base.Message(message);
    public override ObjectSchema Rule(IRule rule) => (ObjectSchema)base.Rule(rule);
    public override ObjectSchema Rule(string name, Rule.ResolverFn resolve) => (ObjectSchema)base.Rule(name, resolve);
    public override ObjectSchema Required() => (ObjectSchema)base.Required();
    public override ObjectSchema Default(object? defaultValue) => (ObjectSchema)base.Default(defaultValue);
    public override ObjectSchema Transform(Func<object?, object?> transform) => (ObjectSchema)base.Transform(transform);

    public ObjectSchema Property(string name, IRule rule)
    {
        Properties[name] = rule;
        return this;
    }

    public ObjectSchema Extend(ObjectSchema schema)
    {
        // add rules that don't already exist
        Rules.AddRange(schema.Rules.Where(r => Rules.Any(rule => rule.Name == r.Name) == false));

        foreach (var (key, rule) in schema.Properties)
        {
            Property(key, rule);
        }

        return this;
    }

    public override IResult<object?> Validate(object? value)
    {
        var res = base.Validate(value);

        if (res.Error != null)
        {
            return res;
        }

        var errors = new ErrorGroup(_message);
        var properties = value?.GetType().GetProperties().Where(p => p.CanRead) ?? [];

        foreach (var (key, rule) in Properties)
        {
            var property = properties.FirstOrDefault(p =>
            {
                var jsonPropertyName = p.GetCustomAttribute<JsonPropertyNameAttribute>();
                var name = jsonPropertyName?.Name ?? p.Name;
                return name == key;
            });

            var result = rule.Resolve(property?.GetValue(value));

            if (result.Error != null)
            {
                errors.Add(Errors.Property(key, result.Error));
            }
            else if (property != null && property.CanWrite)
            {
                property.SetValue(value, result.Value);
            }
        }

        return errors.Empty ? Result<object?>.Ok(value) : Result<object?>.Err(errors);
    }
}