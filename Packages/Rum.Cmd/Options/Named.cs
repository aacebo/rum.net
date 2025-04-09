using System.Text.Json.Serialization;

using Rum.Core;
using Rum.Data;

namespace Rum.Cmd.Options;

/// <summary>
/// Named Command Option
/// </summary>
public interface INamedOption : IOption
{
    /// <summary>
    /// the unique option name
    /// </summary>
    public string Name { get; set; }
}

/// <summary>
/// Named Command Option
/// </summary>
public class Named : INamedOption
{
    [JsonPropertyName("type")]
    [JsonPropertyOrder(0)]
    public ISchema Type { get; set; } = new AnySchema();

    [JsonIgnore]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("alias")]
    [JsonPropertyOrder(1)]
    public IList<string> Aliases { get; set; } = [];

    [JsonPropertyName("description")]
    [JsonPropertyOrder(2)]
    public string? Description { get; set; }

    [JsonPropertyName("transform")]
    [JsonPropertyOrder(3)]
    public Func<object?, object?>? Transform { get; set; }

    public bool Select(string nameOrAlias)
    {
        return Name == nameOrAlias || Aliases.Any(alias => alias == nameOrAlias);
    }

    public IResult<object> Parse(string? arg)
    {
        throw new NotImplementedException();
    }

    public class Builder : IBuilder<Named>
    {
        private readonly Named _value = new();

        public Builder Type(ISchema value)
        {
            _value.Type = value;
            return this;
        }

        public Builder Name(string value)
        {
            _value.Name = value;
            return this;
        }

        public Builder Alias(params string[] value)
        {
            foreach (var item in value)
            {
                _value.Aliases.Add(item);
            }

            return this;
        }

        public Builder Description(string value)
        {
            _value.Description = value;
            return this;
        }

        public Builder Transform(Func<object?, object?> value)
        {
            _value.Transform = value;
            return this;
        }

        public Named Build() => _value;
    }
}