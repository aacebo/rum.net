namespace Rum.Data.Annotations;

public class RequiredAttribute(string? Message = null) : SchemaAttribute(Message)
{
    public override AnySchema Apply(AnySchema schema)
    {
        schema = schema.Required();

        if (Message != null)
        {
            schema = schema.Message(Message);
        }

        return schema;
    }
}