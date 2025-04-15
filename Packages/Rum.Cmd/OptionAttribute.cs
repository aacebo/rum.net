using System.Text.Json;
using System.Text.Json.Serialization;

namespace Rum.Cmd;

[AttributeUsage(
    AttributeTargets.Field | AttributeTargets.Property,
    Inherited = true
)]
public class OptionAttribute(string? Name = null) : Attribute
{
    [JsonPropertyName("name")]
    [JsonPropertyOrder(0)]
    public string? Name { get; set; } = Name;

    [JsonPropertyName("aliases")]
    [JsonPropertyOrder(1)]
    public string[] Aliases { get; set; } = [];

    [JsonPropertyName("description")]
    [JsonPropertyOrder(2)]
    public string? Description { get; set; }

    public bool Select(string key) => key == Name || Aliases.Contains(key);
    public object? Parse(Type type, string? arg)
    {
        if (type == typeof(int?))
        {
            return ParseInt(arg);
        }
        else if (type == typeof(bool?))
        {
            return ParseBool(arg);
        }
        else if (type == typeof(double?))
        {
            return ParseDouble(arg);
        }
        else if (type == typeof(DateTime?))
        {
            return ParseDateTime(arg);
        }

        return arg;
    }

    public int? ParseInt(string? arg)
    {
        if (arg == null) return null;
        if (!int.TryParse(arg, out var parsed))
        {
            throw new ParseException();
        }

        return parsed;
    }

    public bool? ParseBool(string? arg)
    {
        if (arg == null) return true;
        if (!bool.TryParse(arg, out var parsed))
        {
            throw new ParseException();
        }

        return parsed;
    }

    public double? ParseDouble(string? arg)
    {
        if (arg == null) return null;
        if (!double.TryParse(arg, out var parsed))
        {
            throw new ParseException();
        }

        return parsed;
    }

    public DateTime? ParseDateTime(string? arg)
    {
        if (arg == null) return null;
        if (!DateTime.TryParse(arg, out var parsed))
        {
            throw new ParseException();
        }

        return parsed;
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