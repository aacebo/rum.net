using System.Reflection;

using FluentMigrator.Runner;

namespace Rum.Agents.Broker.Extensions;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddMigrations(this IServiceCollection collection)
    {
        return collection.AddFluentMigratorCore()
            .ConfigureRunner(runner =>
            {
                runner.AddPostgres()
                    .WithGlobalConnectionString("Postgres")
                    .ScanIn(Assembly.GetExecutingAssembly())
                    .For.Migrations();
            })
            .AddLogging(logging => logging.AddFluentMigratorConsole());
    }
}