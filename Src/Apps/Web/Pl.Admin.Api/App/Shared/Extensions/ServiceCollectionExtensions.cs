using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace Pl.Admin.Api.App.Shared.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection BaseSetup(this IServiceCollection services)
    {
        services
            .AddControllers(options =>
            {
                options.Filters.Add(new AllowAnonymousFilter());
                options.Filters.Add(new ConsumesAttribute(MediaTypeNames.Application.Json));
            })
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressConsumesConstraintForFormFileParameters = true;
                options.SuppressInferBindingSourcesForParameters = true;
                options.SuppressModelStateInvalidFilter = true;
                options.SuppressMapClientErrors = true;
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.WriteIndented = true;
            });
        return services;
    }

    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddKeycloakWebApiAuthentication(configuration);
        services
            .AddAuthorization(PolicyAuthUtils.RegisterAuthorization)
            .AddKeycloakAuthorization(options =>
            {
                options.RolesResource = configuration.GetSection("Keycloak").GetValue<string>("resource");
                options.EnableRolesMapping = RolesClaimTransformationSource.ResourceAccess;
                options.RoleClaimType = ClaimTypes.Role;
            })
            .AddAuthorizationServer(configuration);

        return services;
    }
}