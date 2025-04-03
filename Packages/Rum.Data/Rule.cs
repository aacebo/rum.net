using System.Text.Json.Serialization;

using Rum.Core;
using Rum.Core.Json;

namespace Rum.Data;

/// <summary>
/// Any Validation Rule
/// </summary>
[JsonConverter(typeof(TrueTypeJsonConverter<IRule>))]
public interface IRule
{
    /// <summary>
    /// the unique rule identifier
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// resolve the input value
    /// </summary>
    /// <param name="value">the input value</param>
    /// <returns>the result, with either an ouput value or error</returns>
    public IResult<object> Resolve(object? value);
}

/// <summary>
/// A Validation Rule
/// </summary>
/// <param name="name">the unqieue rule identifier</param>
/// <param name="resolver">the resolver</param>
public class Rule(string name, Rule.ResolverFn resolver) : IRule
{
    /// <summary>
    /// the unique rule identifier
    /// </summary>
    [JsonPropertyName("name")]
    [JsonPropertyOrder(0)]
    public string Name { get; set; } = name;

    /// <summary>
    /// the rule resolver
    /// </summary>
    public ResolverFn Resolver { get; set; } = resolver;

    /// <summary>
    /// resolve the input value
    /// </summary>
    /// <param name="value">the input value</param>
    /// <returns>the result, with either an ouput value or error</returns>
    public IResult<object> Resolve(object? value)
    {
        return Resolver(value);
    }

    /// <summary>
    /// Rule Resolver
    /// </summary>
    public delegate IResult<object> ResolverFn(object? value);
}