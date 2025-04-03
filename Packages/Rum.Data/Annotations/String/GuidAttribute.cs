namespace Rum.Data.Annotations.String;

[AttributeUsage(
    AttributeTargets.Property | AttributeTargets.Field,
    Inherited = true,
    AllowMultiple = true
)]
public class GuidAttribute(string? Message = null) : SchemaAttribute(Message)
{
    public override AnySchema Apply(AnySchema schema)
    {
        var asString = new StringSchema(schema);
        asString = asString.Guid();

        if (Message != null)
        {
            asString = asString.Message(Message);
        }

        return asString;
    }
}