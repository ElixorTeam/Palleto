using Pl.Admin.Client.Source.Shared.Api.Keycloak.Models;
using Pl.Admin.Models.Features.Admins.Users.Queries;

namespace Pl.Admin.Client.Source.Shared.Api.Web.Models;

public static class UserMapper
{
    public static UserModel DtosToModel (KeycloakUser keycloakUser, UserDto userDto)
    {
        string[] partsOfName = keycloakUser.FirstName.Split(' ');
        return new()
        {
            Id = userDto.Id,
            KcId = keycloakUser.Id,
            Fio = new(
                partsOfName.ElementAtOrDefault(0) ?? string.Empty,
                partsOfName.ElementAtOrDefault(1) ?? string.Empty,
                partsOfName.ElementAtOrDefault(2) ?? string.Empty),
            Username = keycloakUser.Username,
            ProductionSiteId = userDto.ProductionSiteId,
            Roles = []
        };
    }
}