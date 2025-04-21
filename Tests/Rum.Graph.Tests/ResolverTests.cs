using System.Text.Json.Serialization;

using Rum.Graph.Annotations;

namespace Rum.Graph.Tests;

public class ResolverTests
{
    public class UserResolver : Resolver<User>
    {
        [Field("followers")]
        public int GetFollowers()
        {
            return 17;
        }
    }

    public class User
    {
        [JsonPropertyName("id")]
        [JsonPropertyOrder(0)]
        public Guid Id { get; set; } = Guid.NewGuid();

        [JsonPropertyName("name")]
        [JsonPropertyOrder(1)]
        public string Name { get; set; } = string.Empty;

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
        var resolver = new UserResolver();
        var res = await resolver.Resolve(@"{
            id,
            name
        }");

        Assert.False(res.IsError);
        Assert.NotNull(res.Data);
        Assert.IsType<User>(res.Data);
        Assert.Null(res.GetData<User>().Followers);

        res = await resolver.Resolve(@"{
            id,
            name,
            followers
        }");

        Assert.False(res.IsError);
        Assert.NotNull(res.Data);
        Assert.IsType<User>(res.Data);
        Assert.Equal(17, res.GetData<User>().Followers);
    }
}