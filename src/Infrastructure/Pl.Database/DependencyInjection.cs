using Microsoft.Extensions.DependencyInjection;

namespace Pl.Database;

public static class DependencyInjection
{
    public static IServiceCollection AddEfCore(this IServiceCollection services)
    {
        services.AddDbContext<WsDbContext>();

        return services;
    }
}