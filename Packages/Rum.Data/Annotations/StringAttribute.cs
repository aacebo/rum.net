namespace Rum.Data.Annotations;

[AttributeUsage(
    AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Property,
    Inherited = true,
    AllowMultiple = true
)]
public class StringAttribute() : Attribute
{
}