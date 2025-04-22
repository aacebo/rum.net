using System.Reflection;

using Rum.Graph.Contexts;
using Rum.Graph.Exceptions;

namespace Rum.Graph.Annotations;

public class ParamsAttribute : ContextAccessorAttribute
{
    public override object? GetValue(ParamContext context)
    {
        var value = Activator.CreateInstance(context.Parameter.ParameterType);
        var members = context.Parameter.ParameterType
            .GetMembers()
            .Where(member => member is FieldInfo || member is PropertyInfo);

        foreach (var member in members)
        {
            var arg = context.Query.Args.Get(member.GetName());

            if (member is FieldInfo field)
            {
                field.SetValue(value, arg);
            }
            else if (member is PropertyInfo property)
            {
                property.SetValue(value, arg);
            }
        }

        return context.Query.Args;
    }
}