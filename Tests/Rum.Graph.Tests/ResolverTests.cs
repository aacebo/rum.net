using System.Text.Json;
using System.Text.Json.Serialization;

using Microsoft.Extensions.DependencyInjection;

using Rum.Graph.Annotations;
using Rum.Graph.Extensions;
using Rum.Graph.Resolvers;

namespace Rum.Graph.Tests;

public class ResolverTests
{
    private IServiceProvider Services { get; }

    public ResolverTests()
    {
        var services = new ServiceCollection();
        services.AddResolver<UserResolver>();
        services.AddResolver<AddressResolver>();
        Services = services.BuildServiceProvider();
    }

    [Fact]
    public async Task Should_Resolve()
    {
        var resolver = Services.GetRequiredService<UserResolver>();
        var res = await resolver.Resolve("{id,name}");

        Assert.False(res.IsError);
        Assert.NotNull(res.Data);
        Assert.IsType<User>(res.Data);
        Assert.Null(res.GetData<User>().Followers);

        res = await resolver.Resolve(@"{
            id,
            name,
            followers,
            addresses
        }");

        Assert.False(res.IsError);
        Assert.NotNull(res.Data);
        Assert.IsType<User>(res.Data);
        Assert.Equal(17, res.GetData<User>().Followers);

        Console.WriteLine(JsonSerializer.Serialize(res.Data, new JsonSerializerOptions()
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        }));
    }

    public class UserResolver : ObjectResolver<User>
    {
        [Field("followers")]
        public int? GetFollowers()
        {
            return 17;
        }

        [Field("addresses")]
        public IList<Address> GetAddresses()
        {
            return [
                new()
                {
                    Street = "123 Test St",
                    City = "New York",
                    ZipCode = "11249",
                    State = "New York"
                }
            ];
        }
    }

    public class AddressResolver : ObjectResolver<Address>
    {
        [Field("country")]
        public string? GetCountry()
        {
            return "USA";
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
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("followers")]
        [JsonPropertyOrder(2)]
        public int? Followers { get; set; }

        [JsonPropertyName("addresses")]
        [JsonPropertyOrder(3)]
        public IList<Address> Address { get; set; } = [];

        [JsonPropertyName("created_at")]
        [JsonPropertyOrder(4)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    [Resolver<AddressResolver>]
    public class Address
    {
        [JsonPropertyName("street")]
        [JsonPropertyOrder(0)]
        public string? Street { get; set; }

        [JsonPropertyName("city")]
        [JsonPropertyOrder(1)]
        public string? City { get; set; }

        [JsonPropertyName("state")]
        [JsonPropertyOrder(2)]
        public string? State { get; set; }

        [JsonPropertyName("zipcode")]
        [JsonPropertyOrder(3)]
        public string? ZipCode { get; set; }

        [JsonPropertyName("country")]
        [JsonPropertyOrder(4)]
        public string? Country { get; set; }
    }
}