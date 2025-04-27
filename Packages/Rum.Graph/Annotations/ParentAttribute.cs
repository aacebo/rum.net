using Rum.Graph.Contexts;

namespace Rum.Graph.Annotations;

public class ParentAttribute : ContextAccessorAttribute
{
    public override object? GetValue(ParamContext context)
    {
        return context.Value;
    }
}