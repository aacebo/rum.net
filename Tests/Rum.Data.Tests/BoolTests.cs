using Xunit.Abstractions;

namespace Rum.Data.Tests;

public class BoolTests(ITestOutputHelper output)
{
    [Fact]
    public void Bool_ShouldError()
    {
        var res = new Bool().Validate(1);
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Bool_ShouldSucceed()
    {
        var res = new Bool().Validate(true);

        if (res.Error != null)
        {
            output.WriteLine(res.Error.ToString());
        }

        Assert.Null(res.Error);
        Assert.Equal(true, res.Value);
    }

    [Fact]
    public void Required_ShouldError()
    {
        var res = new Bool().Required().Validate(null);
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Required_ShouldSucceed()
    {
        var res = new Bool().Required().Validate(false);

        if (res.Error != null)
        {
            output.WriteLine(res.Error.Message);
        }

        Assert.Null(res.Error);
        Assert.Equal(false, res.Value);
    }

    [Fact]
    public void Enum_ShouldError()
    {
        var res = new Bool().Enum(false).Validate(true);
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Enum_ShouldSucceed()
    {
        var res = new Bool().Enum(false).Validate(false);

        if (res.Error != null)
        {
            output.WriteLine(res.Error.Message);
        }

        Assert.Null(res.Error);
        Assert.Equal(false, res.Value);
    }

    [Fact]
    public void Default_ShouldUseDefault()
    {
        var res = new Bool().Default(true).Validate(null);

        if (res.Error != null)
        {
            output.WriteLine(res.Error.Message);
        }

        Assert.Null(res.Error);
        Assert.Equal(true, res.Value);
    }

    [Fact]
    public void Default_ShouldNotUseDefault()
    {
        var res = new Bool().Default(true).Validate(false);

        if (res.Error != null)
        {
            output.WriteLine(res.Error.Message);
        }

        Assert.Null(res.Error);
        Assert.Equal(false, res.Value);
    }

    [Fact]
    public void Not_ShouldError()
    {
        var res = new Bool().Not(new Bool().Enum(true)).Validate(true);
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Not_ShouldSucceed()
    {
        var res = new Bool().Not(new Bool().Enum(true)).Validate(false);

        if (res.Error != null)
        {
            output.WriteLine(res.Error.Message);
        }

        Assert.Null(res.Error);
        Assert.Equal(false, res.Value);
    }
}