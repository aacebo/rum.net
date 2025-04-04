namespace Rum.Data.Annotations;

public class EnumAttribute(object[]? Options = null, string? Message = null) : SchemaAttribute(Message)
{
    public object[] Options { get; private set; } = Options ?? [];

    public override AnySchema Apply(AnySchema schema)
    {
        schema = schema.Enum(Options);

        if (Message != null)
        {
            schema = schema.Message(Message);
        }

        return schema;
    }
}