using System.Text.Json;
using System.Text.Json.Serialization;

namespace Rum.Graph.Json;

public class ResolverJsonConverter : JsonConverter<IResolver>
{
    public override IResolver? Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, IResolver value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value.ToSchema(), options);
    }
}