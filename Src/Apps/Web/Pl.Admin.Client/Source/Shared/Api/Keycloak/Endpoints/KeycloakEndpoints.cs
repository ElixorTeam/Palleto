using Pl.Admin.Client.Source.Shared.Api.Keycloak.Models;
using Phetch.Core;
// ReSharper disable ClassNeverInstantiated.Global

namespace Pl.Admin.Client.Source.Shared.Api.Keycloak.Endpoints;

public class KeycloakEndpoints(IKeycloakApi keycloakApi)
{
    public ParameterlessEndpoint<KeycloakUser[]> KeycloakUsersEndpoint { get; } = new(
        keycloakApi.GetAllUsers,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(5) });
}