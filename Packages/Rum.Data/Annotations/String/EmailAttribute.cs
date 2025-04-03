namespace Rum.Data.Annotations.String;

public class EmailAttribute(string? Message = null) : SchemaAttribute(Message)
{
    public override AnySchema Apply(AnySchema schema)
    {
        var asString = new StringSchema(schema);
        asString = asString.Email();

        if (Message != null)
        {
            asString = asString.Message(Message);
        }

        return asString;
    }
}