namespace Rum.Data.Annotations.Tests;

public class RequiredAttributeTests()
{
    public class UserLogin
    {
        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }
    }

    [Fact]
    public void Required_ShouldError()
    {
        var res = Schemas.Validate(new UserLogin());
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Required_ShouldSucceed()
    {
        var res = Schemas.Validate(new UserLogin() { Username = "test", Password = "test" });
        Assert.Null(res.Error);
    }
}