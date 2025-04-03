using Xunit.Abstractions;

namespace Rum.Data.Tests;

public class BoolSchemaTests(ITestOutputHelper output)
{
    [Fact]
    public void Bool_ShouldError()
    {
        var res = Schemas.Bool().Validate(1);
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Bool_ShouldSucceed()
    {
        var res = Schemas.Bool().Validate(true);

        if (res.Error != null)
        {
            output.WriteLine(res.Error.ToString());
        }

        Assert.Null(res.Error);
        Assert.True(res.Value);
    }

    [Fact]
    public void Required_ShouldError()
    {
        var res = Schemas.Bool().Required().Validate(null);
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Required_ShouldSucceed()
    {
        var res = Schemas.Bool().Required().Validate(false);

        if (res.Error != null)
        {
            output.WriteLine(res.Error.GetError());
        }

        Assert.Null(res.Error);
        Assert.False(res.Value);
    }

    [Fact]
    public void Enum_ShouldError()
    {
        var res = Schemas.Bool().Enum(false).Validate(true);
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Enum_ShouldSucceed()
    {
        var res = Schemas.Bool().Enum(false).Validate(false);

        if (res.Error != null)
        {
            output.WriteLine(res.Error.GetError());
        }

        Assert.Null(res.Error);
        Assert.False(res.Value);
    }

    [Fact]
    public void Default_ShouldUseDefault()
    {
        var res = Schemas.Bool().Default(true).Validate(null);

        if (res.Error != null)
        {
            output.WriteLine(res.Error.GetError());
        }

        Assert.Null(res.Error);
        Assert.Equal(true, res.Value);
    }

    [Fact]
    public void Default_ShouldNotUseDefault()
    {
        var res = Schemas.Bool().Default(true).Validate(false);

        if (res.Error != null)
        {
            output.WriteLine(res.Error.GetError());
        }

        Assert.Null(res.Error);
        Assert.False(res.Value);
    }
}