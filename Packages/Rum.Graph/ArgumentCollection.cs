using System.Text.Json;
using System.Text.Json.Serialization;

namespace Rum.Graph;

public class ArgumentCollection : Dictionary<string, object?>
{
    public bool Empty => Count == 0;

    public bool Exists(string key) => ContainsKey(key);
    public void Set(string key, object? value) => this[key] = value;
    public object? Get(string key) => this[key];
    public T Get<T>(string key) => (T?)this[key] ?? throw new KeyNotFoundException();
    public T? GetOrDefault<T>(string key) => (T?)Get(key);

    public override string ToString()
    {
        return JsonSerializer.Serialize(this, new JsonSerializerOptions()
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        });
    }
}