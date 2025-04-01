using System.Text.Json;
using System.Text.Json.Serialization;

namespace Rum.Core.Json;

/// <summary>
/// JsonConverter that writes using the
/// values concrete type
/// </summary>
public class TrueTypeJsonConverter<T> : JsonConverter<T> where T : notnull
{
    public override T? Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}