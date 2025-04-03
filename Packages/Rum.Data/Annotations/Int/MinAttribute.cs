namespace Rum.Data.Annotations.Int;

public class MinAttribute(int Value, string? Message = null) : SchemaAttribute(Message)
{
    public int Value { get; private set; } = Value;

    public override AnySchema Apply(AnySchema schema)
    {
        var asInt = new IntSchema(schema);
        asInt = asInt.Min(Value);

        if (Message != null)
        {
            asInt = asInt.Message(Message);
        }

        return asInt;
    }
}