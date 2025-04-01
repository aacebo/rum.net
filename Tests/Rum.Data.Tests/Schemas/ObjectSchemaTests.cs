using Xunit.Abstractions;

namespace Rum.Data.Tests;

public class ObjectSchemaTests(ITestOutputHelper output)
{
    [Fact]
    public void Object_ShouldError()
    {
        var schema = Schemas.Object().Property("test", Schemas.Int().Required());

        var res = schema.Validate(new { test = "hello world" });
        Assert.NotNull(res.Error);

        res = schema.Validate(new { test = true });
        Assert.NotNull(res.Error);

        res = schema.Validate(new { tester = 10 });
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Object_ShouldSucceed()
    {
        var schema = Schemas.Object().Property("test", Schemas.Int().Required());
        var value = new { test = 10 };
        var res = schema.Validate(value);

        if (res.Error != null)
        {
            output.WriteLine(res.Error.ToString());
        }

        Assert.Null(res.Error);
        Assert.Equal(value, res.Value);
    }
}