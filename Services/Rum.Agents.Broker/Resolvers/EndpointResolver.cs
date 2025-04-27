using Rum.Graph;
using Rum.Graph.Annotations;

namespace Rum.Agents.Broker.Resolvers;

public class EndpointResolver : Resolver<Models.Endpoint>
{
    public EndpointResolver(IServiceProvider provider) : base(provider)
    {

    }

    [Field("endpoints")]
    public IList<Endpoint> GetEndpoints()
    {

    }
}