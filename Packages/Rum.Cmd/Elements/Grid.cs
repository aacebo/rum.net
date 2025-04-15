using System.Text;
using System.Text.Json.Serialization;

namespace Rum.Cmd.Elements;

public class Grid : IElement
{
    public int Rows { get; set; } = 0;

    [JsonPropertyName("columns")]
    [JsonPropertyOrder(0)]
    public IList<Column> Columns { get; set; } = [];

    public Grid Add(Column column)
    {
        Columns.Add(column);
        Rows = column.Items.Count;
        return this;
    }

    public string Render()
    {
        var builder = new StringBuilder();

        for (var i = 0; i < Rows; i++)
        {
            List<string> row = [];

            foreach (var column in Columns)
            {
                var item = column.Items[i];
                var rendered = item.Render();

                if (rendered.Length < column.Width)
                {
                    rendered = rendered.PadRight(column.Width - rendered.Length);
                }

                row.Add(rendered);
            }

            builder.AppendLine(string.Join('\t', row));
        }

        return builder.ToString();
    }
}