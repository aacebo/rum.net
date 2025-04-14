using System.Text.Json.Serialization;

using Rum.Core;
using Rum.Data;

namespace Rum.Cmd.Options;

/// <summary>
/// Positional Command Option
/// </summary>
public interface IPositionalOption : IOption
{
    /// <summary>
    /// the position index of the option
    /// (0 indexed)
    /// </summary>
    public int Index { get; set; }
}

/// <summary>
/// Positional Command Option
/// </summary>
public class Positional : IPositionalOption
{
    [JsonPropertyName("type")]
    [JsonPropertyOrder(0)]
    public ISchema Type { get; set; } = new AnySchema();

    [JsonIgnore]
    public int Index { get; set; } = 0;

    [JsonPropertyName("description")]
    [JsonPropertyOrder(2)]
    public string? Description { get; set; }

    [JsonPropertyName("transform")]
    [JsonPropertyOrder(3)]
    public Func<object?, object?>? Transform { get; set; }

    public bool Select(string nameOrAlias)
    {
        return int.TryParse(nameOrAlias, out int index) && Index == index;
    }

    public IResult<object> Parse(string? arg)
    {
        return Type.Validate(arg);
    }

    public class Builder : IBuilder<Positional>
    {
        private readonly Positional _value = new();

        public Builder Type(ISchema value)
        {
            _value.Type = value;
            return this;
        }

        public Builder Index(int value)
        {
            _value.Index = value;
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

        public Positional Build() => _value;
    }
}