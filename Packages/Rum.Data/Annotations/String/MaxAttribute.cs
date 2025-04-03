namespace Rum.Data.Annotations.String;

[AttributeUsage(
    AttributeTargets.Property | AttributeTargets.Field,
    Inherited = true,
    AllowMultiple = true
)]
public class MaxAttribute(int Value, string? Message = null) : SchemaAttribute(Message)
{
    public int Value { get; private set; } = Value;

    public override AnySchema Apply(AnySchema schema)
    {
        var asString = new StringSchema(schema);
        asString = asString.Max(Value);

        if (Message != null)
        {
            asString = asString.Message(Message);
        }

        return asString;
    }
}