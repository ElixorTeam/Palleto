using Phetch.Core;
using Pl.Admin.Client.Source.Shared.Api.Keycloak;
using Pl.Admin.Client.Source.Shared.Api.Keycloak.Models;
using Pl.Admin.Client.Source.Shared.Api.Web.Models;
using Pl.Admin.Models;
using Pl.Admin.Models.Features.Admins.PalletMen.Queries;
using Pl.Admin.Models.Features.Admins.Users.Queries;
// ReSharper disable ClassNeverInstantiated.Global

namespace Pl.Admin.Client.Source.Shared.Api.Web.Endpoints;

public class AdminEndpoints(IWebApi webApi, IKeycloakApi keycloakApi)
{
    public ParameterlessEndpoint<UserModel[]> UsersEndpoint { get; } = new(
        async() =>
        {
            KeycloakUser[] keycloakUsers = await keycloakApi.GetAllUsers();
            UserDto[] usersRelationShips = await webApi.GetAllUsers();

            Dictionary<Guid, UserDto> userRelationDict = usersRelationShips.ToDictionary(x => x.Id, x => x);
            IEnumerable<UserModel> users = keycloakUsers.Select(keycloakUser =>
                userRelationDict.TryGetValue(keycloakUser.Id, out UserDto? userDto)
                    ? UserMapper.DtosToModel(keycloakUser, userDto)
                    : UserMapper.DtosToModel(keycloakUser, new() { Id = Guid.Empty, ProductionSiteId = Guid.Empty }));
            return users.ToArray();
        },
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(5) });

    public void UpdateUser(UserModel user, Guid productionSiteId) =>
        UsersEndpoint.UpdateQueryData(new(), query => query.Data == null ? [user] :
            query.Data.ReplaceItemBy(user with { ProductionSiteId = productionSiteId }, p => p.KcId == user.KcId).ToArray());

    public void DeleteUser(Guid userId) =>
        UsersEndpoint.UpdateQueryData(new(), query =>
            query.Data == null ? [] : query.Data.Where(x => x.Id != userId).ToArray());

    public Endpoint<Guid, PalletManDto[]> PalletMenEndpoint { get; } = new(
        webApi.GetPalletMenByProductionSite,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public Endpoint<Guid, PalletManDto> PalletManEndpoint { get; } = new(
        webApi.GetPalletManByUid,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public void AddPalletMan(Guid productionSiteId, PalletManDto palletMan)
    {
        PalletMenEndpoint.UpdateQueryData(productionSiteId, query =>
            query.Data == null ? [palletMan] : query.Data.Prepend(palletMan).ToArray());
        PalletManEndpoint.UpdateQueryData(palletMan.Id, _ => palletMan);
    }

    public void UpdatePalletMan(Guid productionSiteId, PalletManDto palletMan)
    {
        PalletMenEndpoint.UpdateQueryData(productionSiteId, query =>
            query.Data == null ? [palletMan] : query.Data.ReplaceItemBy(palletMan, p => p.Id == palletMan.Id).ToArray());
        PalletManEndpoint.UpdateQueryData(palletMan.Id, _ => palletMan);
    }

    public void DeletePalletMan(Guid productionSiteId, Guid palletManId)
    {
        PalletMenEndpoint.UpdateQueryData(productionSiteId, query =>
            query.Data == null ? [] : query.Data.Where(x => x.Id != palletManId).ToArray());
        PalletManEndpoint.Invalidate(palletManId);
    }
}