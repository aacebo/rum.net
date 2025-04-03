namespace Rum.Data.Annotations.Tests;

public class StringAttributeTests()
{
    public class MinMaxTest
    {
        [String.Min(5)]
        [String.Max(7)]
        public string? Username { get; set; }
    }

    public class EmailTest
    {
        [String.Email]
        public string? EmailAddress { get; set; }
    }

    public class UrlTest
    {
        [String.Url]
        public string? Url { get; set; }
    }

    public class PatternTest
    {
        [String.Pattern("^[0-9]+$")]
        public string? Input { get; set; }
    }

    [Fact]
    public void Min_ShouldError()
    {
        var res = Schemas.Validate(new MinMaxTest() { Username = "test" });
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Min_ShouldSucceed()
    {
        var res = Schemas.Validate(new MinMaxTest() { Username = "tester" });
        Assert.Null(res.Error);
    }

    [Fact]
    public void Max_ShouldError()
    {
        var res = Schemas.Validate(new MinMaxTest() { Username = "testing123" });
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Max_ShouldSucceed()
    {
        var res = Schemas.Validate(new MinMaxTest() { Username = "tester" });
        Assert.Null(res.Error);
    }

    [Fact]
    public void Email_ShouldError()
    {
        var res = Schemas.Validate(new EmailTest() { EmailAddress = "test" });
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Email_ShouldSucceed()
    {
        var res = Schemas.Validate(new EmailTest() { EmailAddress = "test@test.com" });
        Assert.Null(res.Error);

        res = Schemas.Validate(new EmailTest());
        Assert.Null(res.Error);
    }

    [Fact]
    public void Url_ShouldError()
    {
        var res = Schemas.Validate(new UrlTest() { Url = "test" });
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Url_ShouldSucceed()
    {
        var res = Schemas.Validate(new UrlTest() { Url = "https://google.com" });
        Assert.Null(res.Error);

        res = Schemas.Validate(new UrlTest());
        Assert.Null(res.Error);
    }

    [Fact]
    public void Pattern_ShouldError()
    {
        var res = Schemas.Validate(new PatternTest() { Input = "test" });
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Pattern_ShouldSucceed()
    {
        var res = Schemas.Validate(new PatternTest() { Input = "1234" });
        Assert.Null(res.Error);

        res = Schemas.Validate(new PatternTest());
        Assert.Null(res.Error);
    }
}