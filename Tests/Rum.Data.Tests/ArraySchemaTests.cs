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
        var res = Schemas.Array(Schemas.String().Min(5)).Validate(new List<object>() { "test" });
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Array_SingleItem_ShouldSucceed()
    {
        var value = new List<object>() { "test", "hello" };
        var res = Schemas.Array(Schemas.String().Min(4)).Validate(value);

        if (res.Error != null)
        {
            output.WriteLine(res.Error.ToString());
        }

        Assert.Null(res.Error);
        Assert.Equal(value, res.Value);
    }

    [Fact]
    public void Array_Items_ShouldError()
    {
        var schema = Schemas.Array(Schemas.String(), Schemas.Int());
        var res = schema.Validate(new List<object>() { "a", true });
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Array_Items_ShouldSucceed()
    {
        var value = new List<object>() { "test", 20 };
        var schema = Schemas.Array(Schemas.String(), Schemas.Int());
        var res = schema.Validate(value);

        if (res.Error != null)
        {
            output.WriteLine(res.Error.ToString());
        }

        Assert.Null(res.Error);
        Assert.Equal(value, res.Value);
    }

    [Fact]
    public void Array_Min_ShouldError()
    {
        var schema = Schemas.Array(Schemas.String()).Min(2);
        var res = schema.Validate(new List<object>() { "a" });
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Array_Min_ShouldSucceed()
    {
        var value = new List<object>() { "a", "b" };
        var schema = Schemas.Array(Schemas.String()).Min(2);
        var res = schema.Validate(value);

        if (res.Error != null)
        {
            output.WriteLine(res.Error.ToString());
        }

        Assert.Null(res.Error);
        Assert.Equal(value, res.Value);
    }

    [Fact]
    public void Array_Max_ShouldError()
    {
        var schema = Schemas.Array(Schemas.String()).Max(2);
        var res = schema.Validate(new List<object>() { "a", "b", "c" });
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Array_Max_ShouldSucceed()
    {
        var value = new List<object>() { "a", "b" };
        var schema = Schemas.Array(Schemas.String()).Max(2);
        var res = schema.Validate(value);

        if (res.Error != null)
        {
            output.WriteLine(res.Error.ToString());
        }

        Assert.Null(res.Error);
        Assert.Equal(value, res.Value);
    }
}