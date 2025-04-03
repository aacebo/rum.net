namespace Rum.Data.Annotations;

[AttributeUsage(
    AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Property,
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