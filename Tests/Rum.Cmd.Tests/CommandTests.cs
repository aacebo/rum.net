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
        [Positional]
        [Required]
        public string Message { get; set; } = string.Empty;

        [Option("verbose")]
        [Option.Aliases("v")]
        [Default(false)]
        public bool Verbose { get; set; } = false;
    }

    [Fact]
    public void And_ShouldError()
    {
        var schema = Schemas.And(Schemas.String().Min(3), Schemas.String().Max(3));
        var res = schema.Validate("ab");
        Assert.NotNull(res.Error);
        res = schema.Validate("abcd");
        Assert.NotNull(res.Error);
    }
}