using Xunit.Abstractions;

namespace Rum.Data.Tests;

public class NotSchemaTests(ITestOutputHelper output)
{
    [Fact]
    public void Not_ShouldError()
    {
        var schema = Schemas.Not(Schemas.Int(), Schemas.Bool());
        var res = schema.Validate(1);
        Assert.NotNull(res.Error);
        res = schema.Validate(true);
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Not_ShouldSucceed()
    {
        var schema = Schemas.Not(Schemas.Int(), Schemas.Bool());
        var res = schema.Validate("test");

        if (res.Error != null)
        {
            output.WriteLine(res.Error.ToString());
        }

        Assert.Null(res.Error);
        Assert.Equal("test", res.Value);
    }
}