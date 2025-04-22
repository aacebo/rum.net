using System.Reflection;

namespace Rum.Graph.Contexts;

public class ParamContext : Context
{
    public required string Key { get; set; }
    public required ParameterInfo Parameter { get; set; }
}