using System.Reflection;

using Rum.Graph.Contexts;

namespace Rum.Graph.Annotations;

[AttributeUsage(AttributeTargets.Method, Inherited = true)]
public class FieldAttribute(string name) : Attribute
{
    public string Name { get; } = name;
    public string? Description { get; }

    public async Task<Result> Resolve(IResolver resolver, MethodInfo method, IContext context)
    {
        try
        {
            List<object?> parameters = [];
            var errors = new Error() { Key = "args" };

            foreach (var param in method.GetParameters())
            {
                var accessor = param.GetCustomAttribute<ContextAccessorAttribute>() ?? throw new ArgumentException($"'{param.Name}' is not a valid parameter");
                var result = accessor.Resolve(param, new Context()
                {
                    Query = context.Query,
                    Value = context.Value
                });

                if (result.Error is not null)
                {
                    errors.Add(result.Error);
                    continue;
                }

                parameters.Add(result.Data);
            }

            if (errors.Count > 0)
            {
                return Result.Err(errors);
            }

            var res = method.Invoke(resolver, parameters.ToArray());

            if (res is Task task)
            {
                await task.ConfigureAwait(false);
                res = ((dynamic)task).Result;
            }

            return Result.Ok(res);
        }
        catch (Exception ex)
        {
            return Result.Err(Name, ex.ToString());
        }
    }
}