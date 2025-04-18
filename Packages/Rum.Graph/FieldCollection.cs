using System.Text.Json;
using System.Text.Json.Serialization;

namespace Rum.Graph;

public class FieldCollection : Dictionary<string, Query>
{
    public bool Empty => Count == 0;

    public bool Exists(string key) => ContainsKey(key);
    public void Set(string key, Query value) => this[key] = value;
    public Query Get(string key) => this[key];
    public Query? GetOrDefault(string key) => TryGetValue(key, out var value) ? value : null;

    public override string ToString()
    {
        return JsonSerializer.Serialize(this, new JsonSerializerOptions()
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        });
    }
}