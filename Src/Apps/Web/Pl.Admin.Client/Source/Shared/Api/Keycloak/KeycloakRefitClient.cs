using Pl.Admin.Client.Source.Shared.Auth.Handlers;
using Refit;

namespace Pl.Admin.Client.Source.Shared.Api.Keycloak;

internal sealed class KeycloakRefitClient : IRefitClient
{
    public void Configure(WebApplicationBuilder builder)
    {
        IConfigurationSection oidcConfiguration = builder.Configuration.GetSection("Oidc");

        string authority = oidcConfiguration.GetValue<string>("Authority")!;
        string realm = oidcConfiguration.GetValue<string>("Realm")!;
        string keycloakAdminUrl = $"{authority}/admin/realms/{realm}";

        builder.Services.AddRefitClient<IKeycloakApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new(keycloakAdminUrl))
            .AddHttpMessageHandler<ServerAuthorizationMessageHandler>();
    }
}