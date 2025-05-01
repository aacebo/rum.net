using System.ComponentModel.DataAnnotations;

using Rum.Graph.Annotations;
using Rum.Graph.Tests.Models;

namespace Rum.Graph.Tests.Resolvers;

public class UserResolver : Resolver<User>
{
    public UserResolver(IServiceProvider services) : base(services)
    {

    }

    [Field("id")]
    public Guid GetId([Parent] User user)
    {
        return user.Id;
    }

    [Field("name")]
    public string? GetName([Parent] User user)
    {
        return user.Name;
    }

    [Field("followers")]
    public int? GetFollowers()
    {
        return 17;
    }

    [Field("addresses")]
    public Task<IList<Address>> GetAddresses([Param("$filter"), MinLength(1)] string? filter)
    {
        return Task.FromResult<IList<Address>>([
            new()
            {
                Street = "123 Test St",
                City = "New York",
                ZipCode = "11249",
                State = "New York"
            }
        ]);
    }
}