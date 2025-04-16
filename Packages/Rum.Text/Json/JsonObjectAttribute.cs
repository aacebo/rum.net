using System.Text.Json.Serialization;

namespace Rum.Text.Json;

public class JsonObjectAttribute<T>() : JsonConverterAttribute(typeof(JsonObjectConverter<T>)) where T : notnull
{

}