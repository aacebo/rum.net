using Rum.Core;

using Xunit.Abstractions;

namespace Rum.Data.Tests;

public class AnySchemaTests(ITestOutputHelper output)
{
    [Fact]
    public void Required_ShouldError()
    {
        var res = Schemas.Any().Required().Validate(null);
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Required_ShouldSucceed()
    {
        var res = Schemas.Any().Required().Validate("testing");

        if (res.Error != null)
        {
            output.WriteLine(res.Error.GetError());
        }

        Assert.Null(res.Error);
        Assert.Equal("testing", res.Value);
    }

    [Fact]
    public void Message_ShouldError()
    {
        var res = Schemas.Any().Required().Message("a test message").Validate(null);
        Assert.NotNull(res.Error);
        Assert.Equal("a test message", ((ErrorGroup)res.Error).Message);
    }

    [Fact]
    public void Enum_ShouldError()
    {
        var res = Schemas.Any().Enum(1, "test", false).Validate(12);
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Enum_ShouldSucceed()
    {
        var res = Schemas.Any().Enum(1, "test", false).Validate("test");

        if (res.Error != null)
        {
            output.WriteLine(res.Error.GetError());
        }

        Assert.Null(res.Error);
        Assert.Equal("test", res.Value);
    }

    [Fact]
    public void Default_ShouldUseDefault()
    {
        var res = Schemas.Any().Default(1).Validate(null);

        if (res.Error != null)
        {
            output.WriteLine(res.Error.GetError());
        }

        Assert.Null(res.Error);
        Assert.Equal(1, res.Value);
    }

    [Fact]
    public void Default_ShouldNotUseDefault()
    {
        var res = Schemas.Any().Default(1).Validate("test");

        if (res.Error != null)
        {
            output.WriteLine(res.Error.GetError());
        }

        Assert.Null(res.Error);
        Assert.Equal("test", res.Value);
    }

    [Fact]
    public void Not_ShouldError()
    {
        var res = Schemas.Any().Not(Schemas.Int()).Validate(1);
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Not_ShouldSucceed()
    {
        var res = Schemas.Any().Not(Schemas.Int()).Validate(1.2);

        if (res.Error != null)
        {
            output.WriteLine(res.Error.GetError());
        }

        Assert.Null(res.Error);
        Assert.Equal(1.2, res.Value);
    }
}