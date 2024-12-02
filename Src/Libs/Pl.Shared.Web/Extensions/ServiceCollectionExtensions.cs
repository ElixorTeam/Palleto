using BF.Utilities.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pl.Shared.Constants;

namespace Pl.Shared.Web.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection SetupMauiLocalizer(this IServiceCollection services, ConfigurationManager config)
    {
        CultureInfo defaultCulture = new(
        config.GetSection("System")
            .GetValueOrDefault("Language", Cultures.Ru.Name)
        );

        CultureInfo.DefaultThreadCurrentCulture = defaultCulture;
        CultureInfo.DefaultThreadCurrentUICulture = defaultCulture;

        services.AddLocalization();
        services.AddTransient<AcceptLanguageHandler>();

        return services;
    }

    public static IServiceCollection AddUserClaims(this IServiceCollection services)
    {
        services.AddScoped(s =>
        {
            IHttpContextAccessor? httpContextAccessor = s.GetService<IHttpContextAccessor>();
            HttpContext? httpContext = httpContextAccessor?.HttpContext;
            return httpContext?.User ?? new ClaimsPrincipal();
        });

        return services;
    }

    public static IServiceCollection AddValidators<T>(this IServiceCollection services)
    {
        return services.Scan(scan => scan
            .FromAssembliesOf(typeof(T))
            .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Validator")))
            .AsSelf()
            .WithScopedLifetime()
        );
    }

    public static IServiceCollection AddMiddlewares<T>(this IServiceCollection services)
    {
        return services.Scan(scan => scan
            .FromAssembliesOf(typeof(T))
            .AddClasses(i => i.Where(type => type.Name.EndsWith("Middleware")))
            .AsSelf()
            .WithTransientLifetime()
        );
    }

    public static IServiceCollection AddApiServices<T>(this IServiceCollection services)
    {
        return services.Scan(scan => scan
            .FromAssembliesOf(typeof(T))
            .AddClasses(classes => classes.Where(type => type.Name.EndsWith("ApiService")))
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );
    }

    public static IServiceCollection AddRefitEndpoints<T>(this IServiceCollection services)
    {
        return services.Scan(scan => scan
            .FromAssembliesOf(typeof(T))
            .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Endpoints")))
            .AsSelf()
            .WithScopedLifetime()
        );
    }

    public static IServiceCollection AddHelpers<T>(this IServiceCollection services)
    {
        return services.Scan(scan => scan
            .FromAssembliesOf(typeof(T))
            .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Helper")))
            .AsSelf()
            .WithTransientLifetime()
        );
    }

    public static IServiceCollection AddDelegatingHandlers<T>(this IServiceCollection services)
    {
        return services.Scan(scan => scan
            .FromAssembliesOf(typeof(T))
            .AddClasses(classes => classes.AssignableTo<DelegatingHandler>())
            .AsSelf()
            .WithTransientLifetime()
        );
    }
}