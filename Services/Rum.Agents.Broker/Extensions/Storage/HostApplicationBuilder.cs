using Rum.Agents.Broker.Storage;

namespace Rum.Agents.Broker.Extensions;

public static partial class HostApplicationBuilderExtensions
{
    public static IHostApplicationBuilder AddStorage(this IHostApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IUserStorage, UserStorage>();
        return builder;
    }
}