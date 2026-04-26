using Microsoft.Extensions.Localization;
using Pl.Admin.Api.App.Features.Devices.Arms.Impl.Expressions;
using Pl.Admin.Api.App.Features.Devices.Arms.Impl.Models;
using Pl.Admin.Api.App.Shared.Validators.Api;
using Pl.Admin.Models.Features.Devices.Arms.Commands;
using Pl.Database.Entities.Ref.Arms;
using Pl.Shared.Resources;

namespace Pl.Admin.Api.App.Features.Devices.Arms.Impl.Validators;

public class ArmCreateApiValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    : ApiCreateValidator<ArmEntity, ArmCreateDto>
{
    public override async Task ValidateAsync(DbSet<ArmEntity> dbSet, ArmCreateDto dto)
    {
        UqArmProperties uqProperties = new(dto.SystemKey, dto.Name, dto.Number);
        await ValidateProperties(new ArmCreateValidator(wsDataLocalizer), dto);
        await ValidatePredicatesAsync(dbSet, ArmExpressions.GetUqPredicates(uqProperties));
    }
}



