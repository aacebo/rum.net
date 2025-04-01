using Xunit.Abstractions;

namespace Rum.Data.Tests;

public class ArraySchemaTests(ITestOutputHelper output)
{
    [Fact]
    public void Array_ShouldError()
    {
        var res = Schemas.Array().Validate(1);
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Array_ShouldSucceed()
    {
        var value = new List<object>() { "test" };
        var res = Schemas.Array().Validate(value);

        if (res.Error != null)
        {
            output.WriteLine(res.Error.ToString());
        }

        Assert.Null(res.Error);
        Assert.Equal(value, res.Value);
    }

    [Fact]
    public void Array_SingleItem_ShouldError()
    {
        var res = Schemas.Array(Schemas.Int().Min(5)).Validate(new List<object>() { "test" });
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Array_SingleItem_ShouldSucceed()
    {
        var value = new List<object>() { "test" };
        var res = Schemas.Array(Schemas.String().Min(4)).Validate(value);

        if (res.Error != null)
        {
            output.WriteLine(res.Error.ToString());
        }

        Assert.Null(res.Error);
        Assert.Equal(value, res.Value);
    }
}