using Rum.Core;

namespace Rum.Data;

/// <summary>
/// Any Schema
/// </summary>
public partial interface ISchema<T> : IRule
{
    /// <summary>
    /// validate some input value
    /// </summary>
    /// <param name="value">the input value</param>
    /// <returns>the validation result</returns>
    public IResult<T> Validate(object? value);
}