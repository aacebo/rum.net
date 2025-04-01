using Xunit.Abstractions;

namespace Rum.Data.Tests;

public class AndSchemaTests(ITestOutputHelper output)
{
    [Fact]
    public void And_ShouldError()
    {
        var schema = Schemas.And(Schemas.String().Min(3), Schemas.String().Max(3));
        var res = schema.Validate("ab");
        Assert.NotNull(res.Error);
        res = schema.Validate("abcd");
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void And_ShouldSucceed()
    {
        var schema = Schemas.And(Schemas.String().Min(3), Schemas.String().Max(3));
        var res = schema.Validate("abc");

        if (res.Error != null)
        {
            output.WriteLine(res.Error.ToString());
        }

        Assert.Null(res.Error);
        Assert.Equal("abc", res.Value);
    }
}