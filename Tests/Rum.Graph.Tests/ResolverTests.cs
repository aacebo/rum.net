using Microsoft.Extensions.DependencyInjection;

using Rum.Graph.Extensions;
using Rum.Graph.Tests.Models;
using Rum.Graph.Tests.Resolvers;

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
        var res = await resolver.Resolve("{id}");

        if (res.IsError)
        {
            Console.WriteLine(res.ToString());
        }

        Assert.False(res.IsError);
        Assert.NotNull(res.Data);
        Assert.IsType<User>(res.Data);
        Assert.Null(res.GetData<User>().Followers);

        res = await resolver.Resolve(@"{
            id,
            name,
            followers,
            addresses($filter: ""10"") {state,zipcode,country}
        }", new() { Name = "testuser" });

        if (res.IsError)
        {
            Console.WriteLine(res.ToString());
        }

        Assert.False(res.IsError);
        Assert.NotNull(res.Data);
        Assert.IsType<User>(res.Data);

        var user = res.GetData<User>();

        Assert.Equal(17, user.Followers);
        Assert.Single(user.Address);

        var address = user.Address.First();

        Assert.Equal("New York", address.State);
        Assert.Equal("USA", address.Country);
        Assert.Equal("11249", address.ZipCode);
    }
}