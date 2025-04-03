namespace Rum.Data.Annotations;

[AttributeUsage(
    AttributeTargets.Property | AttributeTargets.Field,
    Inherited = true,
    AllowMultiple = true
)]
public abstract class SchemaAttribute(string? Message = null) : Attribute
{
    /// <summary>
    /// the custom error message
    /// </summary>
    public string? Message { get; private set; } = Message;

    /// <summary>
    /// apply the attribute rules
    /// to the schema
    /// </summary>
    public abstract AnySchema Apply(AnySchema schema);
}