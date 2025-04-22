using Rum.Graph.Annotations;
using Rum.Graph.Resolvers;
using Rum.Graph.Tests.Models;

namespace Rum.Graph.Tests.Resolvers;

public class UserResolver : ObjectResolver<User>
{
    public UserResolver(IServiceProvider services) : base(services)
    {

    }

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