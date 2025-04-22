using System.Text.Json;
using System.Text.Json.Serialization;

using Rum.Graph.Annotations;
using Rum.Graph.Tests.Resolvers;

namespace Rum.Graph.Tests.Models;

[Resolver<AddressResolver>]
public class Address
{
    [JsonPropertyName("street")]
    [JsonPropertyOrder(0)]
    public string? Street { get; set; }

    [JsonPropertyName("city")]
    [JsonPropertyOrder(1)]
    public string? City { get; set; }

    [JsonPropertyName("state")]
    [JsonPropertyOrder(2)]
    public string? State { get; set; }

    [JsonPropertyName("zipcode")]
    [JsonPropertyOrder(3)]
    public string? ZipCode { get; set; }

    [JsonPropertyName("country")]
    [JsonPropertyOrder(4)]
    public string? Country { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this, new JsonSerializerOptions()
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        });
    }
}