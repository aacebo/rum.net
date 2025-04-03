namespace Rum.Data.Annotations;

[AttributeUsage(
    AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Property,
    Inherited = true,
    AllowMultiple = true
)]
public class DefaultAttribute(object? value = null) : SchemaAttribute
{
    public object? Value { get; private set; } = value;

    public override AnySchema<object?> Apply(AnySchema<object?> schema)
    {
        return schema.Default(Value);
    }
}