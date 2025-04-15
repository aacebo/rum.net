using System.Text.Json.Serialization;

namespace Rum.Cmd.Elements;

public class Column
{
    public int Width
    {
        get
        {
            int max = 0;

            foreach (var item in Items)
            {
                var width = item.Render().Length;

                if (width > max)
                {
                    max = width;
                }
            }

            return max;
        }
    }

    [JsonPropertyName("items")]
    [JsonPropertyOrder(0)]
    public IList<IElement> Items { get; set; } = [];

    public Column Add(IElement element)
    {
        Items.Add(element);
        return this;
    }
}