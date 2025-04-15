using System.ComponentModel.DataAnnotations;

using Rum.Cmd;

namespace Rum.Agents.Cli.Commands;

[Command("run")]
public class RunCommand : ICommand
{
    [Positional]
    [Required]
    public string Name { get; set; } = string.Empty;

    public void Execute()
    {
        Console.WriteLine($"running task '{Name}'");
    }
}