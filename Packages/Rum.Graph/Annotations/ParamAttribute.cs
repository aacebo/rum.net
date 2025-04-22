using Rum.Graph.Contexts;

namespace Rum.Graph.Annotations;

public class ParamAttribute(string? name = null) : ContextAccessorAttribute
{
    public string? Name { get; } = name;

    public override object? GetValue(ParamContext context)
    {
        var name = Name ?? context.Parameter.Name;
        return name is null ? null : context.Query.Args.Get(name);
    }
}