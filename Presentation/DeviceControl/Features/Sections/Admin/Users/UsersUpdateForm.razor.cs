using Blazor.Heroicons;
using Blazorise;
using DeviceControl.Auth.ClaimsTransform.CacheProviders.Common;
using DeviceControl.Features.Sections.Shared.Form;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.Claim;
using Ws.Domain.Services.Features.ProductionSite;
using Ws.Domain.Services.Features.User;

namespace DeviceControl.Features.Sections.Admin.Users;

public sealed partial class UsersUpdateForm : SectionFormBase<UserEntity>
{
    #region Inject

    [Inject] private Redirector Redirector { get; set; } = null!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private INotificationService NotificationService { get; set; } = null!;
    [Inject] private IClaimService ClaimService { get; set; } = null!;
    [Inject] private IUserService UserService { get; set; } = null!;
    [Inject] private IProductionSiteService ProductionSiteService { get; set; } = null!;
    [Inject] private IClaimsCacheProvider ClaimsCacheProvider { get; set; } = null!;

    #endregion

    private string UserPrefix { get; set; } = "KOLBASA-VS\\";
    private IEnumerable<ClaimEntity> RolesEntities { get; set; } = [];
    private IEnumerable<ProductionSiteEntity> ProductionSite { get; set; } = new List<ProductionSiteEntity>();

    private IEnumerable<ClaimEntity> SelectedRoles
    {
        get => SectionEntity.Claims.ToList();
        set => SectionEntity.Claims = new HashSet<ClaimEntity>(value);
    }

    private IEnumerable<ActionMenuEntry> AdditionalButtons { get; set; } = [];

    protected override void OnInitialized()
    {
        SelectedRoles = SectionEntity.Claims.ToList();
        RolesEntities = ClaimService.GetAll();
        ProductionSite = ProductionSiteService.GetAll();

        AdditionalButtons = AdditionalButtons.Append(
        new()
        {
            Name = Localizer["SectionFormRelogin"], IconName = HeroiconName.User,
            OnClickAction = EventCallback.Factory.Create(this, ReloginCurrentUser)
        });
    }

    private async Task ReloginCurrentUser()
    {
        ReloginUser(SectionEntity);
        await NotificationService.Info("Релогин выполнен");
    }

    private UserEntity ReloginUser(UserEntity user)
    {
        ClaimsCacheProvider.ClearCacheByUserName(user.Name);
        return SectionEntity;
    }

    private async Task DeleteUserWithRelogin()
    {
        ReloginUser(SectionEntity);
        await DeleteItem(UserService.Delete);
    }
}