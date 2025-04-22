using Rum.Graph.Annotations;
using Rum.Graph.Resolvers;
using Rum.Graph.Tests.Models;

namespace Rum.Graph.Tests.Resolvers;

public class AddressResolver : ObjectResolver<Address>
{
    public AddressResolver(IServiceProvider services) : base(services)
    {

    }

    [Field("state")]
    public string? GetState([Parent] Address address)
    {
        return address.State;
    }

    [Field("country")]
    public string? GetCountry()
    {
        return "USA";
    }
}