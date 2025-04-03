namespace Rum.Data.Annotations.Tests;

public class IntAttributeTests()
{
    public class MinMaxTest
    {
        [Int.Min(5)]
        [Int.Max(7)]
        public int? Value { get; set; }
    }

    [Fact]
    public void Min_ShouldError()
    {
        var res = Schemas.Validate(new MinMaxTest() { Value = 4 });
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Min_ShouldSucceed()
    {
        var res = Schemas.Validate(new MinMaxTest() { Value = 5 });
        Assert.Null(res.Error);
    }

    [Fact]
    public void Max_ShouldError()
    {
        var res = Schemas.Validate(new MinMaxTest() { Value = 8 });
        Assert.NotNull(res.Error);
    }

    [Fact]
    public void Max_ShouldSucceed()
    {
        var res = Schemas.Validate(new MinMaxTest() { Value = 7 });
        Assert.Null(res.Error);
    }
}