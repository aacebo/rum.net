using Rum.Cmd.Annotations;
using Rum.Data.Annotations;
using Option = Rum.Cmd.Annotations.Option;

using Xunit.Abstractions;

namespace Rum.Cmd.Tests;

public class CommandTests(ITestOutputHelper output)
{
    [Command("basic")]
    public class BasicCommand
    {
        [Option("message")]
        [Option.Aliases("m")]
        [Required]
        public string? Message { get; set; }

        [Option("verbose")]
        [Option.Aliases("v")]
        [Default(true)]
        public bool Verbose { get; set; } = false;
    }

    [Fact]
    public void And_ShouldError()
    {
        var command = Command.From<BasicCommand>().Run<BasicCommand>(cmd => Task.Run(() =>
        {
            output.WriteLine(cmd.ToString());
        })).Build();

        var res = command.Run<BasicCommand>("-v");

        if (res.Value != null)
        {
            output.WriteLine(res.Value.Verbose.ToString());
        }

        Assert.NotNull(res.Error);
    }
}