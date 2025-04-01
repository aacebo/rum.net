using Xunit.Abstractions;

namespace Rum.Data.Tests;

public class OrSchemaTests(ITestOutputHelper output)
{
    [Fact]
    public void Or_ShouldError()
    {
        var schema = Schemas.Or(Schemas.Int(), Schemas.Bool());
        var res = schema.Validate("test");
        Assert.NotNull(res.Error);
        res = schema.Validate(1.2);
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Or_ShouldSucceed()
    {
        var schema = Schemas.Or(Schemas.Int(), Schemas.Bool());
        var res = schema.Validate(true);

        if (res.Error != null)
        {
            output.WriteLine(res.Error.ToString());
        }

        Assert.Null(res.Error);
        Assert.Equal(true, res.Value);
    }
}