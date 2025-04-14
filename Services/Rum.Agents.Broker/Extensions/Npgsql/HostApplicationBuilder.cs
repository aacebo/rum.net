using Npgsql;

using SqlKata.Compilers;
using SqlKata.Execution;

namespace Rum.Agents.Broker.Extensions;

public static partial class HostApplicationBuilderExtensions
{
    public static IHostApplicationBuilder AddPostgres(this IHostApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("Postgres");

        if (connectionString == null)
        {
            throw new Exception("no postgres connection string found");
        }

        builder.Services.AddSingleton(_ => new NpgsqlConnection(connectionString));
        builder.Services.AddSingleton(provider =>
        {
            var compiler = new PostgresCompiler();
            var connection = provider.GetRequiredService<NpgsqlConnection>();
            return new QueryFactory(connection, compiler);
        });

        return builder;
    }
}