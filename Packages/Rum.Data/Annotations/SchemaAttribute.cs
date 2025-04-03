namespace Rum.Data.Annotations;

[AttributeUsage(
    AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Property,
    Inherited = true,
    AllowMultiple = true
)]
public abstract class SchemaAttribute() : Attribute
{
    /// <summary>
    /// apply the attribute rules
    /// to the schema
    /// </summary>
    public abstract AnySchema<object?> Apply(AnySchema<object?> schema);
}