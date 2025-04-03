namespace Rum.Data.Annotations;

[AttributeUsage(
    AttributeTargets.Property | AttributeTargets.Field,
    Inherited = true,
    AllowMultiple = true
)]
public class RequiredAttribute() : SchemaAttribute
{
    public override AnySchema<object?> Apply(AnySchema<object?> schema)
    {
        return schema.Required();
    }
}