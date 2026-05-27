using Microsoft.Extensions.DependencyInjection;
using Pl.Database.Options;

namespace Pl.Database;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPlDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        DatabaseOptions databaseOptions =
            configuration.GetRequiredSection(DatabaseOptions.Database).Get<DatabaseOptions>()
            ?? throw new InvalidOperationException($"{DatabaseOptions.Database} is missing in configuration");

        services.AddDbContext<WsDbContext>((_, options) =>
        {
            options.UseSqlServer(databaseOptions.ConnectionString);

            if (ConfigurationUtils.IsDevelop && databaseOptions.IsShowSql)
            {
                options.UseLoggerFactory(
                    LoggerFactory.Create(builder => builder.AddConsole())
                );
            }
        });

        return services;
    }
}