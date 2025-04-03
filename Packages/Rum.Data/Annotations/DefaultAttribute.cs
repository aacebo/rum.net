namespace Rum.Data.Annotations;

public class DefaultAttribute(object Value, string? Message = null) : SchemaAttribute(Message)
{
    public object Value { get; private set; } = Value;

    public override AnySchema Apply(AnySchema schema)
    {
        schema = schema.Default(Value);

        if (Message != null)
        {
            schema = schema.Message(Message);
        }

        return schema;
    }
}