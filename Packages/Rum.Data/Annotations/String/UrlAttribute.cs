namespace Rum.Data.Annotations.String;

[AttributeUsage(
    AttributeTargets.Property | AttributeTargets.Field,
    Inherited = true,
    AllowMultiple = true
)]
public class UrlAttribute(string? Message = null) : SchemaAttribute(Message)
{
    public override AnySchema Apply(AnySchema schema)
    {
        var asString = new StringSchema(schema);
        asString = asString.Url();

        if (Message != null)
        {
            asString = asString.Message(Message);
        }

        return asString;
    }
}