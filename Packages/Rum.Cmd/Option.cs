using System.Text.Json.Serialization;

using Rum.Core;
using Rum.Core.Json;

namespace Rum.Cmd;

/// <summary>
/// Command Option
/// </summary>
[JsonConverter(typeof(TrueTypeJsonConverter<IOption>))]
public interface IOption
{
    /// <summary>
    /// method used to determine if the option
    /// matches the name or alias provided
    /// </summary>
    public bool Select(string nameOrAlias);

    /// <summary>
    /// parse the options argument
    /// </summary>
    public IResult<object> Parse(string? arg);
}
