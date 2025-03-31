using Xunit.Abstractions;

namespace Rum.Schemas.Tests;

public class IntTests(ITestOutputHelper output)
{
    [Fact]
    public void Int_ShouldError()
    {
        var res = new Int().Validate(11.5);
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Int_ShouldSucceed()
    {
        var res = new Int().Validate(10);

        if (res.Error != null)
        {
            output.WriteLine(res.Error.ToString());
        }

        Assert.Null(res.Error);
        Assert.Equal(10, res.Value);
    }

    [Fact]
    public void Required_ShouldError()
    {
        var res = new Int().Required().Validate(null);
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Required_ShouldSucceed()
    {
        var res = new Int().Required().Validate(1);

        if (res.Error != null)
        {
            output.WriteLine(res.Error.Message);
        }

        Assert.Null(res.Error);
        Assert.Equal(1, res.Value);
    }

    [Fact]
    public void Enum_ShouldError()
    {
        var res = new Int().Enum(1, 75, -10).Validate(12);
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Enum_ShouldSucceed()
    {
        var res = new Int().Enum(1, 75, -10).Validate(-10);

        if (res.Error != null)
        {
            output.WriteLine(res.Error.Message);
        }

        Assert.Null(res.Error);
        Assert.Equal(-10, res.Value);
    }

    [Fact]
    public void Default_ShouldUseDefault()
    {
        var res = new Int().Default(1).Validate(null);

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
        var res = new Int().Default(1).Validate(8000);

        if (res.Error != null)
        {
            output.WriteLine(res.Error.Message);
        }

        Assert.Null(res.Error);
        Assert.Equal(8000, res.Value);
    }

    [Fact]
    public void Min_ShouldError()
    {
        var res = new Int().Min(50).Validate(49);
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Min_ShouldSucceed()
    {
        var res = new Int().Min(50).Validate(50);

        if (res.Error != null)
        {
            output.WriteLine(res.Error.Message);
        }

        Assert.Null(res.Error);
        Assert.Equal(50, res.Value);
    }

    [Fact]
    public void Max_ShouldError()
    {
        var res = new Int().Max(50).Validate(51);
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Max_ShouldSucceed()
    {
        var res = new Int().Max(50).Validate(50);

        if (res.Error != null)
        {
            output.WriteLine(res.Error.Message);
        }

        Assert.Null(res.Error);
        Assert.Equal(50, res.Value);
    }

    [Fact]
    public void MultipleOf_ShouldError()
    {
        var res = new Int().MultipleOf(20).Validate(6);
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void MultipleOf_ShouldSucceed()
    {
        var res = new Int().MultipleOf(20).Validate(5);

        if (res.Error != null)
        {
            output.WriteLine(res.Error.Message);
        }

        Assert.Null(res.Error);
        Assert.Equal(5, res.Value);
    }

    [Fact]
    public void Positive_ShouldError()
    {
        var res = new Int().Positive().Validate(-1);
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Positive_ShouldSucceed()
    {
        var res = new Int().Positive().Validate(0);

        if (res.Error != null)
        {
            output.WriteLine(res.Error.Message);
        }

        Assert.Null(res.Error);
        Assert.Equal(0, res.Value);
    }

    [Fact]
    public void Negative_ShouldError()
    {
        var res = new Int().Negative().Validate(0);
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Negative_ShouldSucceed()
    {
        var res = new Int().Negative().Validate(-1);

        if (res.Error != null)
        {
            output.WriteLine(res.Error.Message);
        }

        Assert.Null(res.Error);
        Assert.Equal(-1, res.Value);
    }

    [Fact]
    public void Even_ShouldError()
    {
        var res = new Int().Even().Validate(19);
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Even_ShouldSucceed()
    {
        var res = new Int().Even().Validate(20);

        if (res.Error != null)
        {
            output.WriteLine(res.Error.Message);
        }

        Assert.Null(res.Error);
        Assert.Equal(20, res.Value);
    }

    [Fact]
    public void Odd_ShouldError()
    {
        var res = new Int().Odd().Validate(20);
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Odd_ShouldSucceed()
    {
        var res = new Int().Odd().Validate(19);

        if (res.Error != null)
        {
            output.WriteLine(res.Error.Message);
        }

        Assert.Null(res.Error);
        Assert.Equal(19, res.Value);
    }

    [Fact]
    public void Not_ShouldError()
    {
        var res = new Int().Not(new Int().Even()).Validate(2);
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Not_ShouldSucceed()
    {
        var res = new Int().Not(new Int().Even()).Validate(3);

        if (res.Error != null)
        {
            output.WriteLine(res.Error.Message);
        }

        Assert.Null(res.Error);
        Assert.Equal(3, res.Value);
    }
}