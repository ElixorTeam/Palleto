using Pl.Admin.Client.Source.Shared.Api.Keycloak.Models;
using Refit;

namespace Pl.Admin.Client.Source.Shared.Api.Keycloak;

public interface IKeycloakApi
{
    [Get("/users")]
    Task<KeycloakUser[]> GetAllUsers();

    [Post("/users/{userId}/logout")]
    Task LogoutUser(Guid userId);
}