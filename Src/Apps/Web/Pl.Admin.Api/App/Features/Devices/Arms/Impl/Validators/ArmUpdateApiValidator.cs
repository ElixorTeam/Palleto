using Microsoft.Extensions.Localization;
using Pl.Admin.Api.App.Features.Devices.Arms.Impl.Expressions;
using Pl.Admin.Api.App.Features.Devices.Arms.Impl.Models;
using Pl.Admin.Api.App.Shared.Validators.Api;
using Pl.Admin.Models.Features.Devices.Arms.Commands;
using Pl.Database.Entities.Ref.Arms;
using Pl.Shared.Resources;

namespace Pl.Admin.Api.App.Features.Devices.Arms.Impl.Validators;

public class ArmUpdateApiValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    : ApiUpdateValidator<ArmEntity, ArmUpdateDto, Guid>
{
    public override async Task<ArmEntity> ValidateAndGetAsync(DbSet<ArmEntity> dbSet, ArmUpdateDto dto, Guid id)
    {
        UqArmProperties uqProperties = new(dto.SystemKey, dto.Name, dto.Number);
        await ValidateProperties(new ArmUpdateValidator(wsDataLocalizer), dto);
        return await ValidatePredicatesAsync(dbSet, ArmExpressions.GetUqPredicates(uqProperties), i => i.Id == id);
    }
}