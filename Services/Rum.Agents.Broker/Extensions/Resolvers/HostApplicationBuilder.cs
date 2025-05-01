using Rum.Agents.Broker.Resolvers;

namespace Rum.Agents.Broker.Extensions;

public static partial class HostApplicationBuilderExtensions
{
    public static IHostApplicationBuilder AddResolvers(this IHostApplicationBuilder builder)
    {
        builder.Services.AddSingleton<AgentResolver>();
        return builder;
    }
}