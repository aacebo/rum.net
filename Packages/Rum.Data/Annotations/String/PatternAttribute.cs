using System.Text.RegularExpressions;

namespace Rum.Data.Annotations.String;

[AttributeUsage(
    AttributeTargets.Property | AttributeTargets.Field,
    Inherited = true,
    AllowMultiple = true
)]
public class PatternAttribute : SchemaAttribute
{
    public Regex Value { get; private set; }

    public PatternAttribute(string Value, string? Message = null) : base(Message)
    {
        this.Value = new(Value);
    }

    public PatternAttribute(Regex Value, string? Message = null) : base(Message)
    {
        this.Value = Value;
    }

    public override AnySchema Apply(AnySchema schema)
    {
        var asString = new StringSchema(schema);
        asString = asString.Pattern(Value);

        if (Message != null)
        {
            asString = asString.Message(Message);
        }

        return asString;
    }
}