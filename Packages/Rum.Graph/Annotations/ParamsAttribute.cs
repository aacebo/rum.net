using System.Reflection;

using Rum.Graph.Exceptions;

namespace Rum.Graph.Annotations;

public class ParamsAttribute : ContextAccessorAttribute
{
    public override Result Resolve(ParameterInfo param, IContext context)
    {
        var value = Activator.CreateInstance(param.ParameterType);
        var members = param.ParameterType
            .GetMembers()
            .Where(member => member is FieldInfo || member is PropertyInfo);

        foreach (var member in members)
        {
            var arg = context.Query.Args.Get(member.GetName());
            member.SetValue(value, arg);
        }

        return Result.Ok(value);
    }
}