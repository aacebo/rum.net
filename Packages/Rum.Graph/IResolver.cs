using System.Text.Json.Serialization;

using Rum.Graph.Json;

namespace Rum.Graph;

[JsonConverter(typeof(ResolverJsonConverter))]
public interface IResolver
{
    public string Name { get; }

    public Task<Result> Resolve(IContext context);
    public Schema ToSchema();
}