using System.Text.Json;

using Rum.Cmd;

namespace Rum.Agents.Cli.Commands;

[Command("rum")]
public class RootCommand : ICommand
{
    [Command("run")]
    public RunCommand _Run { get; set; } = new();

    public void Run()
    {
        Console.WriteLine(JsonSerializer.Serialize(this, new JsonSerializerOptions()
        {
            WriteIndented = true
        }));
    }
}