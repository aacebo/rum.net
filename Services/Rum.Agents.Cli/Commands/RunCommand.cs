using System.ComponentModel.DataAnnotations;

using Rum.Cmd;

namespace Rum.Agents.Cli.Commands;

[Command("run")]
public class RunCommand : ICommand
{
    [Positional]
    [Required]
    [MinLength(1)]
    public string? Name { get; set; }

    [Option("verbose", Aliases = ["v"])]
    public bool Verbose { get; set; } = false;

    [Option("test", Aliases = ["t"])]
    public bool Test { get; set; } = false;

    public void Execute()
    {
        Console.WriteLine($"running task '{Name}'");
    }
}