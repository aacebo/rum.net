using Rum.Core;

namespace Rum.Data;

/// <summary>
/// Any Schema
/// </summary>
public partial interface ISchema
{
    /// <summary>
    /// the schemas name
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// validate some input value
    /// </summary>
    /// <param name="value">the input value</param>
    /// <returns>the validation result</returns>
    public IResult<object> Validate(object? value);
}