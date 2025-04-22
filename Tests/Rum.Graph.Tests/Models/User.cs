using System.Text.Json;
using System.Text.Json.Serialization;

using Rum.Graph.Annotations;
using Rum.Graph.Tests.Resolvers;

namespace Rum.Graph.Tests.Models;

[Resolver<UserResolver>]
public class User
{
    [JsonPropertyName("id")]
    [JsonPropertyOrder(0)]
    public Guid Id { get; set; } = Guid.NewGuid();

    [JsonPropertyName("name")]
    [JsonPropertyOrder(1)]
    public string? Name { get; set; }

    [JsonPropertyName("followers")]
    [JsonPropertyOrder(2)]
    public int? Followers { get; set; }

    [JsonPropertyName("addresses")]
    [JsonPropertyOrder(3)]
    public IList<Address> Address { get; set; } = [];

    [JsonPropertyName("created_at")]
    [JsonPropertyOrder(4)]
    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    public override string ToString()
    {
        return JsonSerializer.Serialize(this, new JsonSerializerOptions()
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        });
    }
}