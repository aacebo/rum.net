using System.Text.RegularExpressions;

using Xunit.Abstractions;

namespace Rum.Schemas.Tests;

public class StringTests(ITestOutputHelper output)
{
    [Fact]
    public void String_ShouldError()
    {
        var res = new String().Validate(1);
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void String_ShouldSucceed()
    {
        var res = new String().Validate("testing");

        if (res.Error != null)
        {
            output.WriteLine(res.Error.ToString());
        }

        Assert.Null(res.Error);
        Assert.Equal("testing", res.Value);
    }

    [Fact]
    public void Required_ShouldError()
    {
        var res = new String().Required().Validate(null);
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Required_ShouldSucceed()
    {
        var res = new String().Required().Validate("hi");

        if (res.Error != null)
        {
            output.WriteLine(res.Error.Message);
        }

        Assert.Null(res.Error);
        Assert.Equal("hi", res.Value);
    }

    [Fact]
    public void Enum_ShouldError()
    {
        var res = new String().Enum("hello", "world").Validate("hi");
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Enum_ShouldSucceed()
    {
        var res = new String().Enum("hello", "world").Validate("world");

        if (res.Error != null)
        {
            output.WriteLine(res.Error.Message);
        }

        Assert.Null(res.Error);
        Assert.Equal("world", res.Value);
    }

    [Fact]
    public void Default_ShouldUseDefault()
    {
        var res = new String().Default("default").Validate(null);

        if (res.Error != null)
        {
            output.WriteLine(res.Error.Message);
        }

        Assert.Null(res.Error);
        Assert.Equal("default", res.Value);
    }

    [Fact]
    public void Default_ShouldNotUseDefault()
    {
        var res = new String().Default("default").Validate("hi");

        if (res.Error != null)
        {
            output.WriteLine(res.Error.Message);
        }

        Assert.Null(res.Error);
        Assert.Equal("hi", res.Value);
    }

    [Fact]
    public void Min_ShouldError()
    {
        var res = new String().Min(5).Validate("test");
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Min_ShouldSucceed()
    {
        var res = new String().Min(5).Validate("testing");

        if (res.Error != null)
        {
            output.WriteLine(res.Error.ToString());
        }

        Assert.Null(res.Error);
        Assert.Equal("testing", res.Value);
    }

    [Fact]
    public void Max_ShouldError()
    {
        var res = new String().Max(5).Validate("testing");
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Max_ShouldSucceed()
    {
        var res = new String().Max(5).Validate("test");

        if (res.Error != null)
        {
            output.WriteLine(res.Error.ToString());
        }

        Assert.Null(res.Error);
        Assert.Equal("test", res.Value);
    }

    [Fact]
    public void Length_ShouldError()
    {
        var res = new String().Length(5).Validate("hi");
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Length_ShouldSucceed()
    {
        var res = new String().Length(5).Validate("hello");

        if (res.Error != null)
        {
            output.WriteLine(res.Error.ToString());
        }

        Assert.Null(res.Error);
        Assert.Equal("hello", res.Value);
    }

    [Fact]
    public void Pattern_String_ShouldError()
    {
        var res = new String().Pattern("[0-9]+").Validate("hi");
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Pattern_String_ShouldSucceed()
    {
        var res = new String().Pattern("[0-9]+").Validate("123");

        if (res.Error != null)
        {
            output.WriteLine(res.Error.ToString());
        }

        Assert.Null(res.Error);
        Assert.Equal("123", res.Value);
    }

    [Fact]
    public void Pattern_Regex_ShouldError()
    {
        var res = new String().Pattern(new Regex("[0-9]+")).Validate("hi");
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Pattern_Regex_ShouldSucceed()
    {
        var res = new String().Pattern(new Regex("[0-9]+")).Validate("123");

        if (res.Error != null)
        {
            output.WriteLine(res.Error.ToString());
        }

        Assert.Null(res.Error);
        Assert.Equal("123", res.Value);
    }

    [Fact]
    public void Email_ShouldError()
    {
        var res = new String().Email().Validate("hi");
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Email_ShouldSucceed()
    {
        var res = new String().Email().Validate("test@gmail.com");

        if (res.Error != null)
        {
            output.WriteLine(res.Error.ToString());
        }

        Assert.Null(res.Error);
        Assert.Equal("test@gmail.com", res.Value);
    }

    [Fact]
    public void Guid_ShouldError()
    {
        var res = new String().Guid().Validate("hi");
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Guid_ShouldSucceed()
    {
        var res = new String().Guid().Validate("076db1e2-c222-4663-a195-b4c7a556e1fd");

        if (res.Error != null)
        {
            output.WriteLine(res.Error.ToString());
        }

        Assert.Null(res.Error);
        Assert.Equal("076db1e2-c222-4663-a195-b4c7a556e1fd", res.Value);
    }

    [Fact]
    public void Url_ShouldError()
    {
        var res = new String().Url().Validate("hi");
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Url_ShouldSucceed()
    {
        var res = new String().Url().Validate("http://localhost");

        if (res.Error != null)
        {
            output.WriteLine(res.Error.ToString());
        }

        Assert.Null(res.Error);
        Assert.Equal("http://localhost", res.Value);
    }

    [Fact]
    public void Not_ShouldError()
    {
        var res = new String().Not(new String().Enum("a", "b")).Validate("a");
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Not_ShouldSucceed()
    {
        var res = new String().Not(new String().Enum("a", "b")).Validate("c");

        if (res.Error != null)
        {
            output.WriteLine(res.Error.Message);
        }

        Assert.Null(res.Error);
        Assert.Equal("c", res.Value);
    }
}