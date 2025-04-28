using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Rum.Graph.Annotations;

public class ParamAttribute(string? name = null) : ContextAccessorAttribute
{
    public string? Name { get; } = name;

    public override Result Resolve(ParameterInfo param, IContext context)
    {
        var name = Name ?? param.Name ?? param.Position.ToString();
        var value = context.Query.Args.Get(name);

        if (value is null)
        {
            return !param.IsOptional ? Result.Err(name, "required") : Result.Ok();
        }

        if (!value.GetType().IsAssignableTo(param.ParameterType))
        {
            return Result.Err(
                param.Position.ToString(),
                $"expected type \"{param.ParameterType.Name}\", received \"{value.GetType().Name}\""
            );
        }

        var attributes = param.GetCustomAttributes<ValidationAttribute>();
        var results = new Collection<ValidationResult>();
        var validContext = new ValidationContext(value)
        {
            DisplayName = name,
            MemberName = param.Member.Name
        };

        if (!Validator.TryValidateValue(value, validContext, results, attributes))
        {
            var errors = new Error() { Key = name };

            foreach (var result in results)
            {
                errors.Add(new Error()
                {
                    Message = result.ErrorMessage ?? result.ToString()
                });
            }

            return Result.Err(errors);
        }

        return Result.Ok(value);
    }
}