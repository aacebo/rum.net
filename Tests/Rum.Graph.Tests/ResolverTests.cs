using Rum.Graph.Annotations;

namespace Rum.Graph.Tests;

public class ResolverTests
{
    [Schema]
    public class TestSchema
    {
        [Schema.Field("id")]
        public string Id { get; set; } = "test";

        [Schema.Field("name")]
        public string GetName([Param] string id)
        {
            return string.Empty;
        }
    }

    [Fact]
    public async Task Should_ResolveObject()
    {
        var res = await GraphResolver.Resolve<TestSchema>(@"{
            id,
            name
        }");

        Assert.False(res.IsError);
        Assert.NotNull(res.Data);

        var data = res.TryGetData<TestSchema>();

        Assert.NotNull(data);
    }
}