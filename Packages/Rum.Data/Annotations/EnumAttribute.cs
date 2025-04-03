namespace Rum.Data.Annotations;

[AttributeUsage(
    AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Property,
    Inherited = true,
    AllowMultiple = true
)]
public class EnumAttribute(params object?[] options) : SchemaAttribute
{
    public object?[] Options { get; private set; } = options;

    public override AnySchema<object?> Apply(AnySchema<object?> schema)
    {
        return schema.Enum(Options);
    }
}