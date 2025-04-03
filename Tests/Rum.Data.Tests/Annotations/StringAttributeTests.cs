namespace Rum.Data.Annotations.Tests;

public class StringAttributeTests()
{
    public class UserLogin
    {
        [String.Min(5)]
        public string? Username { get; set; }
    }

    [Fact]
    public void Min_ShouldError()
    {
        var res = Schemas.Validate(new UserLogin() { Username = "test" });
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Min_ShouldSucceed()
    {
        var res = Schemas.Validate(new UserLogin() { Username = "tester" });
        Assert.Null(res.Error);
    }
}