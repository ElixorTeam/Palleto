using Phetch.Core;
using Pl.Admin.Models;
using Pl.Admin.Models.Features.Admins.PalletMen.Queries;
using Pl.Admin.Models.Features.Admins.Users.Queries;
// ReSharper disable ClassNeverInstantiated.Global

namespace Pl.Admin.Client.Source.Shared.Api.Web.Endpoints;

public class AdminEndpoints(IWebApi webApi)
{
    public ParameterlessEndpoint<UserDto[]> UserRelationshipEndpoint { get; } = new(
        webApi.GetAllUsers,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(5) });

    public void UpdateUserRelationship(UserDto user) =>
        UserRelationshipEndpoint.UpdateQueryData(new(), query =>
        {
            if (query.Data == null) return query.Data!;
            UserDto? dto = query.Data.FirstOrDefault(item => item.Id == user.Id);
            return dto == null ? query.Data.Append(user).ToArray() : query.Data.ReplaceItemBy(user, p => p.Id == user.Id).ToArray();
        });

    public void DeleteUserRelationship(Guid userId) =>
        UserRelationshipEndpoint.UpdateQueryData(new(), query =>
            query.Data == null ? query.Data! : query.Data.Where(x => x.Id != userId).ToArray());

    public Endpoint<Guid, PalletManDto[]> PalletMenEndpoint { get; } = new(
        webApi.GetPalletMenByProductionSite,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public void AddPalletMan(Guid productionSiteId, PalletManDto palletMan) =>
        PalletMenEndpoint.UpdateQueryData(productionSiteId, query =>
            query.Data == null ? query.Data! : query.Data.Prepend(palletMan).ToArray());

    public void UpdatePalletMan(Guid productionSiteId, PalletManDto palletMan) =>
        PalletMenEndpoint.UpdateQueryData(productionSiteId, query =>
            query.Data == null ? query.Data! : query.Data.ReplaceItemBy(palletMan, p => p.Id == palletMan.Id).ToArray());

    public void DeletePalletMan(Guid productionSiteId, Guid palletManId) =>
        PalletMenEndpoint.UpdateQueryData(productionSiteId, query =>
            query.Data == null ? query.Data! : query.Data.Where(x => x.Id != palletManId).ToArray());
}