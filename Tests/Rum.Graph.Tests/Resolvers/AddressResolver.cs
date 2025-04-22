using Rum.Graph.Annotations;
using Rum.Graph.Resolvers;
using Rum.Graph.Tests.Models;

namespace Rum.Graph.Tests.Resolvers;

public class AddressResolver : ObjectResolver<Address>
{
    public AddressResolver(IServiceProvider services) : base(services)
    {

    }

    [Field("country")]
    public string? GetCountry()
    {
        Console.WriteLine("!!HIT!!");
        return "USA";
    }
}