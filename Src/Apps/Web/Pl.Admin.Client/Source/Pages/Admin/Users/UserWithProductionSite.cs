using Pl.Admin.Client.Source.Shared.Api.Keycloak.Models;

namespace Pl.Admin.Client.Source.Pages.Admin.Users;

public record UserWithProductionSite(KeycloakUser User, Guid ProductionSiteId);