using DeviceControl.Features.Sections.Shared.Form;
using DeviceControl.Resources;
using Force.DeepCloner;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Services.Features.Claim;

namespace DeviceControl.Features.Sections.Admin.Users;

public sealed partial class UsersCreateForm: SectionFormBase<UserEntity>
{
    #region Inject
    
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private IClaimService ClaimService { get; set; } = null!;
    
    #endregion

    private string UserPrefix { get; set; } = "KOLBASA-VS\\";

    private IEnumerable<ClaimEntity> RolesEntities { get; set; } = [];
    private IEnumerable<ClaimEntity> SelectedRolesInternal { get; set; } = [];
    
    private IEnumerable<ClaimEntity> SelectedRoles
    {
        get => SelectedRolesInternal;
        set
        {
            SectionEntity.Claims = new HashSet<ClaimEntity>(value);
            SelectedRolesInternal = value;
        }
    }

    protected override void OnInitialized()
    {
        SectionEntity.Name = UserPrefix;
        SelectedRolesInternal = SectionEntity.Claims.ToList();
        RolesEntities = ClaimService.GetAll();
    }

    private UserEntity ProcessItem(UserEntity item)
    {
        UserEntity userEntity = SectionEntity.DeepClone();
        string userName = userEntity.Name;
        userName = userName.ToUpper();
        if (!userName.Contains(UserPrefix))
            userName += UserPrefix;
        userEntity.Name = userName;
        return userEntity;
    }
}