namespace Rum.Graph.Annotations;

[AttributeUsage(
    AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method,
    Inherited = true
)]
public abstract class MiddlewareAttribute : Attribute
{
    public abstract Task<object?> Invoke(IContext context, CancellationToken cancellationToken = default);
}