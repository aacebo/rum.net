using System.Text.Json.Serialization;

using Rum.Graph.Annotations;

namespace Rum.Graph.Tests;

public class ResolverTests
{
    public class UserResolver
    {
        [Field("followers")]
        public int Followers()
        {
            return 17;
        }
    }

    [Resolver<UserResolver>]
    public class User
    {
        [JsonPropertyName("id")]
        [JsonPropertyOrder(0)]
        public Guid Id { get; set; } = Guid.NewGuid();

        [JsonPropertyName("name")]
        [JsonPropertyOrder(1)]
        public required string Name { get; set; }

        [JsonPropertyName("followers")]
        [JsonPropertyOrder(2)]
        public int? Followers { get; set; }

        [JsonPropertyName("created_at")]
        [JsonPropertyOrder(3)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
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