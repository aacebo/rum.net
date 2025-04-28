using System.Reflection;

namespace Rum.Graph.Annotations;

public class ParentAttribute : ContextAccessorAttribute
{
    public override Result Resolve(ParameterInfo param, IContext context)
    {
        return Result.Ok(context.Value);
    }
}