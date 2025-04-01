using Xunit.Abstractions;

namespace Rum.Data.Tests;

public class DoubleSchemaTests(ITestOutputHelper output)
{
    [Fact]
    public void Double_ShouldSucceed()
    {
        var res = Schemas.Double().Validate(10.5);

        if (res.Error != null)
        {
            output.WriteLine(res.Error.ToString());
        }

        Assert.Null(res.Error);
        Assert.Equal(10.5, res.Value);
    }

    [Fact]
    public void Required_ShouldError()
    {
        var res = Schemas.Double().Required().Validate(null);
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Required_ShouldSucceed()
    {
        var res = Schemas.Double().Required().Validate(1.1);

        if (res.Error != null)
        {
            output.WriteLine(res.Error.GetError());
        }

        Assert.Null(res.Error);
        Assert.Equal(1.1, res.Value);
    }

    [Fact]
    public void Enum_ShouldError()
    {
        var res = Schemas.Double().Enum(1.1, 2.2).Validate(3.3);
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Enum_ShouldSucceed()
    {
        var res = Schemas.Double().Enum(1.1, 2.2).Validate(2.2);

        if (res.Error != null)
        {
            output.WriteLine(res.Error.GetError());
        }

        Assert.Null(res.Error);
        Assert.Equal(2.2, res.Value);
    }

    [Fact]
    public void Default_ShouldUseDefault()
    {
        var res = Schemas.Double().Default(1.1).Validate(null);

        if (res.Error != null)
        {
            output.WriteLine(res.Error.GetError());
        }

        Assert.Null(res.Error);
        Assert.Equal(1.1, res.Value);
    }

    [Fact]
    public void Default_ShouldNotUseDefault()
    {
        var res = Schemas.Double().Default(1.3).Validate(8000.0);

        if (res.Error != null)
        {
            output.WriteLine(res.Error.GetError());
        }

        Assert.Null(res.Error);
        Assert.Equal(8000.0, res.Value);
    }

    [Fact]
    public void Min_ShouldError()
    {
        var res = Schemas.Double().Min(50.3).Validate(50.2);
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Min_ShouldSucceed()
    {
        var res = Schemas.Double().Min(50.1).Validate(50.1);

        if (res.Error != null)
        {
            output.WriteLine(res.Error.GetError());
        }

        Assert.Null(res.Error);
        Assert.Equal(50.1, res.Value);
    }

    [Fact]
    public void Max_ShouldError()
    {
        var res = Schemas.Double().Max(50.3).Validate(50.4);
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Max_ShouldSucceed()
    {
        var res = Schemas.Double().Max(50.3).Validate(50.3);

        if (res.Error != null)
        {
            output.WriteLine(res.Error.GetError());
        }

        Assert.Null(res.Error);
        Assert.Equal(50.3, res.Value);
    }

    [Fact]
    public void Positive_ShouldError()
    {
        var res = Schemas.Double().Positive().Validate(-1);
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Positive_ShouldSucceed()
    {
        var res = Schemas.Double().Positive().Validate(0);

        if (res.Error != null)
        {
            output.WriteLine(res.Error.GetError());
        }

        Assert.Null(res.Error);
        Assert.Equal(0, res.Value);
    }

    [Fact]
    public void Negative_ShouldError()
    {
        var res = Schemas.Double().Negative().Validate(0);
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Negative_ShouldSucceed()
    {
        var res = Schemas.Double().Negative().Validate(-1);

        if (res.Error != null)
        {
            output.WriteLine(res.Error.GetError());
        }

        Assert.Null(res.Error);
        Assert.Equal(-1, res.Value);
    }
}