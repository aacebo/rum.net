namespace Rum.Data.Annotations.String;

public class LengthAttribute(int Value, string? Message = null) : SchemaAttribute(Message)
{
    public int Value { get; private set; } = Value;

    public override AnySchema Apply(AnySchema schema)
    {
        var asString = new StringSchema(schema);
        asString = asString.Length(Value);

        if (Message != null)
        {
            asString = asString.Message(Message);
        }

        return asString;
    }
}