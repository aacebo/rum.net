using Xunit.Abstractions;

namespace Rum.Schemas.Tests;

public class AnyTests(ITestOutputHelper output)
{
    [Fact]
    public void Required_ShouldError()
    {
        var res = new Any().Required().Validate(null);
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Required_ShouldSucceed()
    {
        var res = new Any().Required().Validate("testing");

        if (res.Error != null)
        {
            output.WriteLine(res.Error.Message);
        }

        Assert.Null(res.Error);
        Assert.Equal("testing", res.Value);
    }

    [Fact]
    public void Enum_ShouldError()
    {
        var res = new Any().Enum(1, "test", false).Validate(12);
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Enum_ShouldSucceed()
    {
        var res = new Any().Enum(1, "test", false).Validate("test");

        if (res.Error != null)
        {
            output.WriteLine(res.Error.Message);
        }

        Assert.Null(res.Error);
        Assert.Equal("test", res.Value);
    }

    [Fact]
    public void Default_ShouldUseDefault()
    {
        var res = new Any().Default(1).Validate(null);

        if (res.Error != null)
        {
            output.WriteLine(res.Error.Message);
        }

        Assert.Null(res.Error);
        Assert.Equal(1, res.Value);
    }

    [Fact]
    public void Default_ShouldNotUseDefault()
    {
        var res = new Any().Default(1).Validate("test");

        if (res.Error != null)
        {
            output.WriteLine(res.Error.Message);
        }

        Assert.Null(res.Error);
        Assert.Equal("test", res.Value);
    }
}